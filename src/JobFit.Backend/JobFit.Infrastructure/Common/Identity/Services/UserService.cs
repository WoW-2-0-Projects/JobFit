using System.Linq.Expressions;
using JobFit.Application.Common.Identity.Services;
using JobFit.Domain.Common.Commands;
using JobFit.Domain.Common.Queries;
using JobFit.Domain.Entities;
using JobFit.Persistence.Repositories.Interfaces;

namespace JobFit.Infrastructure.Common.Identity.Services;

/// <summary>
/// Provides user foundation service functionality
/// </summary>
public class UserService(IUserRepository userRepository) : IUserService
{
    public IQueryable<User> Get(Expression<Func<User, bool>>? predicate = default, QueryOptions queryOptions = default) =>
        userRepository.Get(predicate, queryOptions);

    public ValueTask<User?> GetByIdAsync(Guid userId, QueryOptions queryOptions = default, CancellationToken cancellationToken = default) =>
        userRepository.GetByIdAsync(userId, queryOptions, cancellationToken);

    public ValueTask<User> CreateAsync(User user, CommandOptions commandOptions = default, CancellationToken cancellationToken = default) =>
        userRepository.CreateAsync(user, commandOptions, cancellationToken);

    public ValueTask<User> UpdateAsync(User user, CommandOptions commandOptions = default, CancellationToken cancellationToken = default) =>
        userRepository.UpdateAsync(user, commandOptions, cancellationToken);

    public ValueTask<User?> DeleteByIdAsync(Guid userId, CommandOptions commandOptions = default, CancellationToken cancellationToken = default) =>
        userRepository.DeleteByIdAsync(userId, commandOptions, cancellationToken);

    public ValueTask<User?> DeleteAsync(User user, CommandOptions commandOptions = default, CancellationToken cancellationToken = default) =>
        userRepository.DeleteAsync(user, commandOptions, cancellationToken);
}