using JobFit.Domain.Entities;

namespace JobFit.Application.Common.FileStorage.Services;

/// <summary>
/// Defines storage file processing service functionality.
/// </summary>
public interface IStorageFileProcessingService
{
    /// <summary>
    /// Creates a new storage file.
    /// </summary>
    /// <param name="stream">The file stream to copy file content from</param>
    /// <param name="fileName">The original name of file being uploaded </param>
    /// <param name="contentType">The content </param>
    /// <param name="ownerId">The ID of the owner that has permissions to the file</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>The created storage file.</returns>
    ValueTask<StorageFile> CreateAsync(
        Stream stream,
        string fileName,
        string contentType,
        Guid ownerId,
        CancellationToken cancellationToken = default);
}