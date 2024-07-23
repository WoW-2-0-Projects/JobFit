using JobFit.Domain.Common.Entities;

namespace JobFit.Domain.Entities;

/// <summary>
/// Represents a stored file in the system.
/// </summary>
public sealed class StorageFile : EntityBase, IAuditableEntity
{
    /// <summary>
    /// Gets or sets the original name of the file.
    /// </summary>
    public string OriginalName { get; set; } = default!;

    /// <summary>
    /// Gets or sets the physical name of the file.
    /// </summary>
    public string PhysicalName { get; set; } = default!;

    /// <summary>
    /// Gets or sets the content type of the file.
    /// </summary>
    public string ContentType { get; set; } = default!;

    /// <summary>
    /// Gets or sets the extension of the file.
    /// </summary>
    public string Extension { get; set; } = default!;
    
    /// <summary>
    /// Gets or sets a flag indicating whether to save the file only in cache.
    /// </summary>
    public bool IsTemporaryFile { get; set; }

    /// <summary>
    /// Gets or sets the ID of the file owner
    /// </summary>
    public Guid OwnerId { get; set; }

    /// <summary>
    /// Gets or sets the file size
    /// </summary>
    public ulong Size { get; set; }
    
    public Employee Owner { get; set; } = default!;

    /// <summary>
    /// Gets or sets the path parameters.
    /// </summary>
    public Dictionary<string, string> PathVariables { get; set; } = default!;

    public DateTimeOffset CreatedTime { get; set; }

    public DateTimeOffset? ModifiedTime { get; set; }

    public string CacheKey => Id.ToString();
}