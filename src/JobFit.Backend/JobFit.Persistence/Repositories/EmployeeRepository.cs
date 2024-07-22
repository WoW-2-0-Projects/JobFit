using System.Linq.Expressions;
using JobFit.Domain.Common.Commands;
using JobFit.Domain.Common.Queries;
using JobFit.Domain.Entities;
using JobFit.Persistence.DataContext;
using JobFit.Persistence.Repositories.Interfaces;

namespace JobFit.Persistence.Repositories;

public class EmployeeRepository(AppDbContext dbContext) : EntityRepositoryBase<Employee, AppDbContext>(dbContext), IEmployeeRepository
{
    public new IQueryable<Employee> Get(Expression<Func<Employee, bool>>? predicate = default, QueryOptions queryOptions = default)
    {
        return base.Get(predicate, queryOptions);
    }

    public new ValueTask<Employee?> GetByIdAsync(Guid employeeId, QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default)
    {
        return base.GetByIdAsync(employeeId, queryOptions, cancellationToken);
    }

    public new ValueTask<Employee> CreateAsync(Employee employee, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return base.CreateAsync(employee, commandOptions, cancellationToken);
    }

    public new ValueTask<Employee> UpdateAsync(Employee employee, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return base.UpdateAsync(employee, commandOptions, cancellationToken);
    }

    public new ValueTask<Employee?> DeleteByIdAsync(Guid employeeId, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return base.DeleteByIdAsync(employeeId, commandOptions, cancellationToken);
    }

    public new ValueTask<Employee?> DeleteAsync(Employee employee, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return base.DeleteAsync(employee, commandOptions, cancellationToken);
    }
}