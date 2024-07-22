using System.Linq.Expressions;
using JobFit.Domain.Common.Commands;
using JobFit.Domain.Common.Queries;
using JobFit.Domain.Entities;
using JobFit.Persistence.DataContext;
using JobFit.Persistence.Repositories.Interfaces;

namespace JobFit.Persistence.Repositories;

public class UserRepository(AppDbContext dbContext) : EntityRepositoryBase<User,AppDbContext>(dbContext), IUserRepository
{
    public new IQueryable<User> Get(Expression<Func<User, bool>>? predicate = default, QueryOptions queryOptions = default)
    {
        return base.Get(predicate, queryOptions);
    }

    public new ValueTask<User?> GetByIdAsync(Guid userId, QueryOptions queryOptions = default, CancellationToken cancellationToken = default)
    {
        return base.GetByIdAsync(userId, queryOptions, cancellationToken);
    }

    public new ValueTask<User> CreateAsync(User user, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return base.CreateAsync(user, commandOptions, cancellationToken);
    }

    public new ValueTask<User> UpdateAsync(User user, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return base.UpdateAsync(user, commandOptions, cancellationToken);
    }

    public new ValueTask<User?> DeleteByIdAsync(Guid userId, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return base.DeleteByIdAsync(userId, commandOptions, cancellationToken);
    }

    public new ValueTask<User?> DeleteAsync(User user, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return base.DeleteAsync(user, commandOptions, cancellationToken);
    }
}