namespace JobFit.Application.Common.FileStorage.Brokers;

/// <summary>
/// Defines the functionality for providing file content types.
/// </summary>
public interface IFileContentTypeProvider
{
    /// <summary>
    /// Gets file extension based on file name or content type.
    /// </summary>
    /// <param name="contentType">The content type of a file.</param>
    /// <param name="fileName">The file original name</param>
    /// <returns>Extracted file extension</returns>
    string GetExtension(string contentType, string? fileName = null);
    
    /// <summary>
    /// Checks if the content type is valid.
    /// </summary>
    /// <param name="contentType">The content type of a file.</param>
    /// <returns>True if content type is valid.</returns>
    bool IsValidContentType(string contentType);

    /// <summary>
    /// Checks if the extension is valid.
    /// </summary>
    /// <param name="extension">The extension of a file.</param>
    /// <returns>True if extension is valid.</returns>
    bool IsValidExtension(string extension);

    /// <summary>
    /// Checks if the content type and extensions matches.
    /// </summary>
    /// <param name="contentType">The content type of a file.</param>
    /// <param name="extension">The extension of a file.</param>
    /// <returns>True if both content type and extension matches.</returns>
    bool IsMatchingContentTypeAndExtension(string contentType, string extension);
}