using System.Linq.Expressions;
using JobFit.Domain.Common.Commands;
using JobFit.Domain.Common.Queries;
using JobFit.Domain.Entities;
using JobFit.Persistence.Caching.Brokers;
using JobFit.Persistence.Caching.Models;
using JobFit.Persistence.DataContext;
using JobFit.Persistence.Extensions;
using JobFit.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JobFit.Persistence.Repositories;

/// <summary>
/// Provides repository functionality for managing storage files.
/// </summary>
public class StorageFileRepository(AppDbContext dbContext, ICacheBroker cacheBroker)
    : EntityRepositoryBase<StorageFile, AppDbContext>(dbContext), IStorageFileRepository
{
    public new IQueryable<StorageFile> Get(Expression<Func<StorageFile, bool>>? predicate = default, QueryOptions queryOptions = default)
    {
        return base.Get(predicate, queryOptions);
    }

    public async ValueTask<List<StorageFile>> GetByIdsAsync(
        ICollection<Guid> fileIds,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default)
    {
        // TODO : Optimize entity query and cache query

        // Query from local entities snapshot
        var files = DbContext.Set<StorageFile>().Local
            .Where(file => fileIds.Contains(file.Id))
            .ToList();

        // Check if all files are found
        var existingFileIds = files.Select(file => file.Id).ToHashSet();
        var missingFileIds = fileIds.Except(existingFileIds).ToList();
        if (!missingFileIds.Any())
            return files;

        // Query missing files from cache and track
        files.AddRange((await Task.WhenAll(missingFileIds
                .Select(async fileId => (FileId: fileId, File: await cacheBroker.GetAsync<StorageFile>(fileId.ToString(), cancellationToken)))))
            .Where(file => file.File is not null)
            .Select(file =>
            {
                if (!file.File!.IsTemporaryFile)
                    DbContext.Entry(file.File).ApplyTrackingMode(queryOptions.TrackingMode);

                return file.File;
            })
            .ToList());

        // Check if all files are found
        existingFileIds = files.Select(file => file.Id).ToHashSet();
        missingFileIds = fileIds.Except(existingFileIds).ToList();
        if (!missingFileIds.Any())
            return files;

        // Query missing files from database
        files.AddRange(await Get(file => missingFileIds.Contains(file.Id), queryOptions)
            .ToListAsync(cancellationToken: cancellationToken));

        return files;
    }

    public new async ValueTask<StorageFile?> GetByIdAsync(
        Guid fileId,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default
    )
    {
        // Query from local entities snapshot
        var file = DbContext.Set<StorageFile>().Local.FirstOrDefault(file => file.Id == fileId);
        if (file != null)
            return file;

        // Query from cache
        file = await cacheBroker.GetAsync<StorageFile>(fileId.ToString(), cancellationToken);
        if (file != null)
        {
            // Set tracking mode if the file is not a temporary file
            if (!file.IsTemporaryFile)
                DbContext.Entry(file).ApplyTrackingMode(queryOptions.TrackingMode);

            return file;
        }

        // Query from database
        file = await base.GetByIdAsync(fileId, queryOptions, cancellationToken);

        return file;
    }

    public new async ValueTask<StorageFile> CreateAsync(
        StorageFile file,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        // Store file in database only if it's not a temporary file
        if (file.IsTemporaryFile)
        {
            // Store in cache
            file.Id = Guid.NewGuid();
        }

        return file;
    }

    public new async ValueTask<StorageFile?> DeleteByIdAsync(Guid fileId, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        // Get file by ID
        var existingFile = await GetByIdAsync(fileId, cancellationToken: cancellationToken);
        if (existingFile is null)
            throw new FileNotFoundException(fileId.ToString(),nameof(StorageFile));

        // If file is not a temporary file, delete from database
            existingFile = await base.DeleteByIdAsync(fileId, commandOptions, cancellationToken);

        return existingFile;
    }

    public new async ValueTask<StorageFile?> DeleteAsync(StorageFile file, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        // If file is not a temporary file, delete from database
        var deletedFile = default(StorageFile?);

        deletedFile = await base.DeleteAsync(file, commandOptions, cancellationToken);

        return deletedFile;
    }
}