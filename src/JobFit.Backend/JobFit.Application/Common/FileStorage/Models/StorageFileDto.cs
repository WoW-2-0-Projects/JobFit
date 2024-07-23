namespace JobFit.Application.Common.FileStorage.Models;

/// <summary>
/// Represents a stored file data transfer object.
/// </summary>
public sealed record StorageFileDto
{
    /// <summary>
    /// Gets file ID.
    /// </summary>
    public Guid Id { get; init; }
    
    /// <summary>
    /// Gets or sets the original name of the file.
    /// </summary>
    public string OriginalName { get; init; } = default!;
    
    /// <summary>
    /// Gets or sets the ID of the file owner
    /// </summary>
    public Guid OwnerId { get; init; }

    /// <summary>
    /// Gets or sets the file size
    /// </summary>
    public ulong Size { get; init; }

    /// <summary>
    /// Gets the file absolute url.
    /// </summary>
    public string FileAbsoluteUrl { get; init; } = default!;
}