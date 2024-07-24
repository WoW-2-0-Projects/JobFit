using JobFit.Domain.Entities;

namespace JobFit.Application.Common.FileStorage.Brokers;

/// <summary>
/// Defines the file storage service functionality.
/// </summary>
public interface IFileStorageBroker
{
    /// <summary>
    /// Uploads a file to the storage or moves temporary files to permanent folder.
    /// </summary>
    /// <param name="file">The file to be uploaded.</param>
    /// <param name="fileContentStream">The content stream of file being uploaded</param>
    /// <returns>The path of the uploaded file.</returns>
    ValueTask UploadAsync(StorageFile file, Stream? fileContentStream = default);

    /// <summary>
    /// Gets the absolute URL of a file in the storage.
    /// </summary>
    /// <param name="fileName">The name of the file.</param>
    /// <param name="pathVariables">Optional path variables for the file URL.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the absolute URL of the file.</returns>
    ValueTask<string> GetFileAbsoluteUrlAsync(string fileName);

    /// <summary>
    /// Gets the absolute URL of a file in the storage.
    /// </summary>
    /// <param name="storageFile">The storage file object.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the absolute URL of the file.</returns>
    ValueTask<string> GetFileAbsoluteUrlAsync(StorageFile storageFile);

    /// <summary>
    /// Deletes a file from the storage.
    /// </summary>
    /// <param name="filePath">The path of the file to be deleted.</param>
    ValueTask DeleteFileAsync(string filePath);

    /// <summary>
    /// Gets a file from the storage.
    /// </summary>
    /// <param name="filePath">The path of the file to be retrieved.</param>
    /// <returns>The file as a Stream.</returns>
    ValueTask<Stream> GetFileContentAsync(string filePath);

    /// <summary>
    /// Gets the relative URL of a file in the storage.
    /// </summary>
    /// <param name="fileName">The name of the file.</param>
    /// <returns>The relative URL of the file.</returns>
    string GetFileRelativeUrl(string fileName);
}