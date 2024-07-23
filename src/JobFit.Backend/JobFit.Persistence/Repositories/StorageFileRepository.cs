using System.Linq.Expressions;
using JobFit.Domain.Common.Commands;
using JobFit.Domain.Common.Queries;
using JobFit.Domain.Entities;
using JobFit.Persistence.Caching.Brokers;
using JobFit.Persistence.DataContext;
using JobFit.Persistence.Repositories.Interfaces;

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

        // Query from database
        file = await base.GetByIdAsync(fileId, queryOptions, cancellationToken);

        return file;
    }

    public new async ValueTask<StorageFile> CreateAsync(
        StorageFile file,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return await base.CreateAsync(file, commandOptions, cancellationToken);
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
        return await base.DeleteAsync(file, commandOptions, cancellationToken);
    }
}