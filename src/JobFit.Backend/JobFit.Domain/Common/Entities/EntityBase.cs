namespace JobFit.Domain.Common.Entities;

/// <summary>
/// Represents entity base
/// </summary>
public class EntityBase : IEntity
{
    public Guid Id { get; set; }
}