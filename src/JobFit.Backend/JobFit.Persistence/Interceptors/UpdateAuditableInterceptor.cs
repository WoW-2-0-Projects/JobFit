using JobFit.Domain.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace JobFit.Persistence.Interceptors;

/// <summary>
/// Represents interceptor to update auditable properties of entities before saving changes to the database.
/// </summary>
public class UpdateAuditableInterceptor
    : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is null)
            throw new DbUpdateException("Entity interceptor execution failed, event data context is null.");

        var auditableEntries = eventData.Context.ChangeTracker.Entries<IAuditableEntity>().ToList();

        auditableEntries.ForEach(entry =>
        {
            if (entry.State == EntityState.Modified)
                entry.Property(nameof(IAuditableEntity.ModifiedTime)).CurrentValue = DateTimeOffset.UtcNow;

            if (entry.State == EntityState.Added)
                entry.Property(nameof(IAuditableEntity.CreatedTime)).CurrentValue = DateTimeOffset.UtcNow;
        });

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}