using System.Linq.Expressions;
using JobFit.Domain.Common.Commands;
using JobFit.Domain.Common.Entities;
using JobFit.Domain.Common.Queries;
using JobFit.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace JobFit.Persistence.Repositories;

/// <summary>
/// Represents a base repository for entities with common CRUD operations.
/// </summary>
/// <param name="dbContext"></param>
public abstract class EntityRepositoryBase<TEntity, TContext>(TContext dbContext) where TEntity : class, IEntity where TContext : DbContext
{
    protected TContext DbContext => dbContext;

    /// <summary>
    /// Gets entities from based on optional filtering conditions
    /// </summary>
    /// <param name="predicate">Entity filter predicate</param>
    /// <param name="queryOptions">Query options</param>
    /// <returns>Queryable source entities</returns>
    protected IQueryable<TEntity> Get(Expression<Func<TEntity, bool>>? predicate = default, QueryOptions queryOptions = default)
    {
        var initialQuery = DbContext.Set<TEntity>().Where(entity => true);

        if (predicate is not null)
            initialQuery = initialQuery.Where(predicate);

        return initialQuery.ApplyTrackingMode(queryOptions.TrackingMode);
    }

    /// <summary>
    /// Gets entities by Id
    /// </summary>
    /// <param name="entityId">Entity Id</param>
    /// <param name="queryOptions">Query options</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>Entity if found, otherwise null</returns>
    protected async ValueTask<TEntity?> GetByIdAsync(
        Guid entityId,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default
    )
    {
        var foundEntity = default(TEntity?);

        var initialQuery = DbContext.Set<TEntity>().AsQueryable();

        initialQuery.ApplyTrackingMode(queryOptions.TrackingMode);

        foundEntity = await initialQuery.FirstOrDefaultAsync(entity => entity.Id == entityId, cancellationToken);

        return foundEntity;
    }

    /// <summary>
    /// Checks if an entity exists
    /// </summary>
    /// <param name="queryableSource">Queryable source of the entity.</param>
    /// <param name="expectedValue">Expected value to check against the result</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>True if entity exists, otherwise false</returns>
    protected async ValueTask<bool> CheckAsync<TValue>(IQueryable<TValue> queryableSource, TValue? expectedValue = default,
        CancellationToken cancellationToken = default)
    {
        var result = await queryableSource.FirstOrDefaultAsync(cancellationToken: cancellationToken);

        return result is not null && (expectedValue is not null
            ? result.Equals(expectedValue)
            : !result.Equals(default(TValue)));
    }

    /// <summary>
    /// Checks if entity exists
    /// </summary>
    /// <param name="predicate">Predicate to check entity existence</param>
    /// <param name="memberSelector">Expression that selects the member of entity as a result</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>True if entity exists, otherwise false</returns>
    protected async ValueTask<(bool Result, TProperty Property)> CheckAsync<TProperty>(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, TProperty>> memberSelector,
        CancellationToken cancellationToken = default
    )
    {
        var foundResult = await DbContext
            .Set<TEntity>()
            .Where(predicate)
            .Select(memberSelector)
            .FirstOrDefaultAsync(cancellationToken);

        var isDefault = EqualityComparer<TProperty>.Default.Equals(foundResult, default);

        return (!isDefault, foundResult!);
    }

    /// <summary>
    /// Creates a new entity
    /// </summary>
    /// <param name="entity">Entity to create</param>
    /// <param name="commandOptions">Create command options</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>Created entity</returns>
    protected async ValueTask<TEntity> CreateAsync(
        TEntity entity,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    )
    {
        await DbContext.Set<TEntity>().AddAsync(entity, cancellationToken);

        if (!commandOptions.SkipSavingChanges)
            await DbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }

    /// <summary>
    /// Updates an existing entity
    /// </summary>
    /// <param name="entity">Entity to update</param>
    /// <param name="commandOptions">Update command options</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>Updated entity</returns>
    protected async ValueTask<TEntity> UpdateAsync(TEntity entity, CommandOptions commandOptions, CancellationToken cancellationToken = default)
    {
        DbContext.Set<TEntity>().Update(entity);

        if (!commandOptions.SkipSavingChanges)
            await DbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }

    /// <summary>
    /// Updates entities in batch
    /// </summary>
    /// <param name="batchUpdatePredicate">Predicate to select entities for batch update</param>
    /// <param name="setPropertyCalls">Batch update value selectors</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>Number of updated rows.</returns>
    protected async ValueTask<int> UpdateBatchAsync(
        Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> setPropertyCalls,
        Expression<Func<TEntity, bool>>? batchUpdatePredicate = default,
        CancellationToken cancellationToken = default
    )
    {
        var entities = DbContext.Set<TEntity>().AsQueryable();

        if (batchUpdatePredicate is not null)
            entities = entities.Where(batchUpdatePredicate);

        return await entities.ExecuteUpdateAsync(setPropertyCalls, cancellationToken);
    }

    /// <summary>
    /// Deletes an existing entity by Id
    /// </summary>
    /// <param name="entity">Entity to delete</param>
    /// <param name="commandOptions">Delete command options</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>Updated entity if soft deleted, otherwise null</returns>
    protected async ValueTask<TEntity?> DeleteAsync(
        TEntity entity,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    )
    {
        DbContext.Set<TEntity>().Remove(entity);

        if (!commandOptions.SkipSavingChanges)
            await DbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }

    /// <summary>
    /// Deletes an existing entity by Id
    /// </summary>
    /// <param name="entityId">Id of entity to delete</param>
    /// <param name="commandOptions">Delete command options</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>Deletion result</returns>
    protected async ValueTask<TEntity?> DeleteByIdAsync(Guid entityId, CommandOptions commandOptions, CancellationToken cancellationToken = default)
    {
        var entity = await DbContext.Set<TEntity>().FirstOrDefaultAsync(entity => entity.Id == entityId, cancellationToken) ??
                     throw new InvalidOperationException();

        DbContext.Remove(entity);

        if (!commandOptions.SkipSavingChanges)
            await DbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }
}