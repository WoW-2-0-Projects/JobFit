namespace JobFit.Domain.Common.Entities;

/// <summary>
/// Defines entity interface
/// </summary>
public interface IEntity
{
    /// <summary>
    /// Gets or sets the entity identifier
    /// </summary>
    public Guid Id { get; set; }
}