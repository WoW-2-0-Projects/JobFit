using System.Linq.Expressions;
using JobFit.Application.Employees.Services;
using JobFit.Domain.Common.Commands;
using JobFit.Domain.Common.Queries;
using JobFit.Domain.Entities;
using JobFit.Persistence.Repositories.Interfaces;

namespace JobFit.Infrastructure.Employees.Services;

public class EmployeeService(IEmployeeRepository employeeRepository) : IEmployeeService
{
    public IQueryable<Employee> Get(Expression<Func<Employee, bool>>? predicate = default, QueryOptions queryOptions = default)
    {
        return employeeRepository.Get(predicate, queryOptions);
    }

    public ValueTask<Employee?> GetByIdAsync(Guid employeeId, QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default)
    {
        return employeeRepository.GetByIdAsync(employeeId, queryOptions, cancellationToken);
    }

    public ValueTask<Employee> CreateAsync(Employee employee, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return employeeRepository.CreateAsync(employee, commandOptions, cancellationToken);
    }

    public ValueTask<Employee> UpdateAsync(Employee employee, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return employeeRepository.UpdateAsync(employee, commandOptions, cancellationToken);
    }

    public ValueTask<Employee?> DeleteByIdAsync(Guid employeeId, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return employeeRepository.DeleteByIdAsync(employeeId, commandOptions, cancellationToken);
    }

    public ValueTask<Employee?> DeleteAsync(Employee employee, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return employeeRepository.DeleteAsync(employee, commandOptions, cancellationToken);
    }
}