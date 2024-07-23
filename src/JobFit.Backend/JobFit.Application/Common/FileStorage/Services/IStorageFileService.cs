using System.Linq.Expressions;
using JobFit.Domain.Common.Commands;
using JobFit.Domain.Common.Queries;
using JobFit.Domain.Entities;

namespace JobFit.Application.Common.FileStorage.Services;

/// <summary>
/// Defines the foundation service for managing storage files.
/// </summary>
public interface IStorageFileService
{
    /// <summary>
    /// Gets a queryable source of storage files based on an optional predicate and query options.
    /// </summary>
    /// <param name="predicate">Optional predicate to filter storage files.</param>
    /// <param name="queryOptions">Query options</param>
    /// <returns>Queryable source of storage files</returns>
    IQueryable<StorageFile> Get(Expression<Func<StorageFile, bool>>? predicate = default, QueryOptions queryOptions = default);

    /// <summary>
    /// Gets a collection of storage files based on an optional predicate and query options.
    /// </summary>
    /// <param name="fileIds">The collection of file IDs to query.</param>
    /// <param name="queryOptions">Query options</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>Collection of storage files</returns>
    ValueTask<List<StorageFile>> GetByIdsAsync(ICollection<Guid> fileIds, QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a single storage file by its ID.
    /// </summary>
    /// <param name="fileId">The ID of file.</param>
    /// <param name="queryOptions">Query options.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>Storage file if found, otherwise null</returns>
    ValueTask<StorageFile?> GetByIdAsync(Guid fileId, QueryOptions queryOptions = default, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Creates a storage file
    /// </summary>
    /// <param name="file">The storage file to be created.</param>
    /// <param name="commandOptions">Create command options</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>Created storage file</returns>
    ValueTask<StorageFile> CreateAsync(StorageFile file, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Deletes a storage file by their Id
    /// </summary>
    /// <param name="fileId">The Id of the storage file to be deleted.</param>
    /// <param name="commandOptions">Delete command options</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>Updated storage file if soft deleted, otherwise null</returns>
    ValueTask<StorageFile?> DeleteByIdAsync(Guid fileId, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a storage file
    /// </summary>
    /// <param name="file">The storage file to be deleted.</param>
    /// <param name="commandOptions">Delete command options</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>Updated storage file if soft deleted, otherwise null</returns>
    ValueTask<StorageFile?> DeleteAsync(StorageFile file, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);
}