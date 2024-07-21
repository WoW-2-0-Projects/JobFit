using JobFit.Domain.Common.Entities;
using JobFit.Domain.Common.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace JobFit.Persistence.Extensions;

/// <summary>
/// Contains EF core internal logic extensions.
/// </summary>
public static class EfCoreExtensions
{
    /// <summary>
    /// Applies pagination to given query source
    /// </summary>
    /// <param name="source">Queryable source</param>
    /// <param name="trackingMode">Tracking mode to apply</param>
    /// <typeparam name="TSource">Query source type</typeparam>
    /// <returns>Query source with pagination applied</returns>
    public static IQueryable<TSource> ApplyTrackingMode<TSource>(this IQueryable<TSource> source, QueryTrackingMode trackingMode)
        where TSource : class
    {
        return trackingMode switch
        {
            QueryTrackingMode.AsTracking => source,
            QueryTrackingMode.AsNoTracking => source.AsNoTracking(),
            QueryTrackingMode.AsNoTrackingWithIdentityResolution => source.AsNoTrackingWithIdentityResolution(),
            _ => source
        };
    }

    /// <summary>
    /// Applies tracking mode to the entity
    /// </summary>
    /// <param name="entityEntry">Entity entry in db context to track</param>
    /// <param name="trackingMode">The tracking mode to apply</param>
    /// <typeparam name="TEntity">The type of entity</typeparam>
    /// <returns>Changed entity entry</returns>
    public static EntityEntry<TEntity> ApplyTrackingMode<TEntity>(this EntityEntry<TEntity> entityEntry, QueryTrackingMode trackingMode)
        where TEntity : class
    {
        entityEntry.State = trackingMode switch
        {
            QueryTrackingMode.AsNoTracking or QueryTrackingMode.AsNoTrackingWithIdentityResolution => EntityState.Detached,
            QueryTrackingMode.AsTracking => EntityState.Unchanged,
            _ => entityEntry.State
        };

        return entityEntry;
    }

    /// <summary>
    /// Queries the source and returns the filtered entities
    /// </summary>
    /// <param name="source">Original query source</param>
    /// <param name="filteredSource">Filtered query source to get entities Id from</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <typeparam name="TSource">Query source type</typeparam>
    /// <returns>New query with entities Id predicate</returns>
    public static async ValueTask<IQueryable<TSource>> GetFilteredEntitiesQuery<TSource>(
        this IQueryable<TSource> filteredSource,
        IQueryable<TSource> source,
        CancellationToken cancellationToken = default
    ) where TSource : class, IEntity
    {
        var entitiesId = await filteredSource.Select(entity => entity.Id).ToListAsync(cancellationToken: cancellationToken);
        return source.Where(entity => entitiesId.Contains(entity.Id));
    }
}