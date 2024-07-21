namespace JobFit.Domain.Common.Entities;

/// <summary>
/// Defines an entity that created and modified times can be tracked.
/// </summary>
public interface IAuditableEntity
{
    /// <summary>
    /// Gets or sets the date and time when the entity was created.
    /// </summary>
    public DateTimeOffset CreatedTime { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the entity was last modified.
    /// </summary>
    public DateTimeOffset? ModifiedTime { get; set; }
}