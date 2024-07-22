using System.Linq.Expressions;
using JobFit.Domain.Common.Commands;
using JobFit.Domain.Common.Queries;
using JobFit.Domain.Entities;

namespace JobFit.Persistence.Repositories.Interfaces;

/// <summary>
/// Defines the functionality for managing employees.
/// </summary>
public interface IEmployeeRepository
{
    /// <summary>
    /// Gets a queryable source of employees based on an optional predicate and query options.
    /// </summary>
    /// <param name="predicate">Optional predicate to filter employees.</param>
    /// <param name="queryOptions">Query options.</param>
    /// <returns>Queryable source of employees.</returns>
    IQueryable<Employee> Get(Expression<Func<Employee, bool>>? predicate = default, QueryOptions queryOptions = default);

    /// <summary>
    /// Gets a single employee by their unique identifier.
    /// </summary>
    /// <param name="employeeId">The unique identifier of the employee.</param>
    /// <param name="queryOptions">Query options for sorting, paging, etc.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>Employee if found, otherwise null.</returns>
    ValueTask<Employee?> GetByIdAsync(Guid employeeId, QueryOptions queryOptions = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new employee.
    /// </summary>
    /// <param name="employee">The employee to be created.</param>
    /// <param name="commandOptions">Options for the create command.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The created employee.</returns>
    ValueTask<Employee> CreateAsync(Employee employee, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing employee.
    /// </summary>
    /// <param name="employee">The employee to be updated.</param>
    /// <param name="commandOptions">Options for the update command.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The updated employee.</returns>
    ValueTask<Employee> UpdateAsync(Employee employee, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes an employee by their unique identifier.
    /// </summary>
    /// <param name="employeeId">The unique identifier of the employee to be deleted.</param>
    /// <param name="commandOptions">Options for the delete command.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The deleted employee if found, otherwise null.</returns>
    ValueTask<Employee?> DeleteByIdAsync(Guid employeeId, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes an employee.
    /// </summary>
    /// <param name="employee">The employee to be deleted.</param>
    /// <param name="commandOptions">Options for the delete command.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The deleted employee if found, otherwise null.</returns>
    ValueTask<Employee?> DeleteAsync(Employee employee, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);
}
