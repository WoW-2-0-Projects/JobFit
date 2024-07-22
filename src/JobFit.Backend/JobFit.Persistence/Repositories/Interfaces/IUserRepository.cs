using System.Linq.Expressions;
using JobFit.Domain.Common.Commands;
using JobFit.Domain.Common.Queries;
using JobFit.Domain.Entities;

namespace JobFit.Persistence.Repositories.Interfaces;

/// <summary>
/// Defines user repository functionality
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Gets a queryable source of users based on an optional predicate and query options.
    /// </summary>
    /// <param name="predicate">Optional predicate to filter users.</param>
    /// <param name="queryOptions">Query options</param>
    /// <returns>Queryable source of users</returns>
    IQueryable<User> Get(Expression<Func<User, bool>>? predicate = default, QueryOptions queryOptions = default);

    /// <summary>
    /// Gets a single user by their unique identifier.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="queryOptions">Query options for sorting, paging, etc.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>User if found, otherwise null</returns>
    ValueTask<User?> GetByIdAsync(Guid userId, QueryOptions queryOptions = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a user
    /// </summary>
    /// <param name="user">The user to be created.</param>
    /// <param name="commandOptions">Create command options</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Created user</returns>
    ValueTask<User> CreateAsync(User user, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing user
    /// </summary>
    /// <param name="user">The user to be updated.</param>
    /// <param name="commandOptions">Update command options</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Updated user</returns>
    ValueTask<User> UpdateAsync(User user, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a user by their Id
    /// </summary>
    /// <param name="userId">The Id of the user to be deleted.</param>
    /// <param name="commandOptions">Delete command options</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Updated user if soft deleted, otherwise null</returns>
    ValueTask<User?> DeleteByIdAsync(Guid userId, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a user
    /// </summary>
    /// <param name="user">The user to be deleted.</param>
    /// <param name="commandOptions">Delete command options</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Updated user if soft deleted, otherwise null</returns>
    ValueTask<User?> DeleteAsync(User user, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);
}