using System.Linq.Expressions;
using JobFit.Application.Common.FileStorage.Services;
using JobFit.Domain.Common.Commands;
using JobFit.Domain.Common.Queries;
using JobFit.Domain.Entities;
using JobFit.Persistence.Repositories.Interfaces;

namespace JobFit.Infrastructure.Common.FileStorage.Services;

/// <summary>
/// Provides the foundation service functionality for managing storage files.
/// </summary>
public class StorageFileService(IStorageFileRepository storageFileRepository) : IStorageFileService
{
    public IQueryable<StorageFile> Get(Expression<Func<StorageFile, bool>>? predicate = default, QueryOptions queryOptions = default)
    {
        return storageFileRepository.Get(predicate, queryOptions);
    }

    public ValueTask<StorageFile?> GetByIdAsync(Guid fileId, QueryOptions queryOptions = default, CancellationToken cancellationToken = default)
    {
        return storageFileRepository.GetByIdAsync(fileId, queryOptions, cancellationToken);
    }

    public ValueTask<StorageFile> CreateAsync(
        StorageFile file,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return storageFileRepository.CreateAsync(file, commandOptions, cancellationToken);
    }

    public ValueTask<StorageFile?> DeleteByIdAsync(Guid fileId, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return storageFileRepository.DeleteByIdAsync(fileId, commandOptions, cancellationToken);
    }

    public ValueTask<StorageFile?> DeleteAsync(StorageFile file, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return storageFileRepository.DeleteAsync(file, commandOptions, cancellationToken);
    }
}