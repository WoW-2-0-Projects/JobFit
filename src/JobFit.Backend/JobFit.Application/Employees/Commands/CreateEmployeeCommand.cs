using JobFit.Application.Employees.Models;
using JobFit.Domain.Common.Commands;

namespace JobFit.Application.Employees.Commands;

public class CreateEmployeeCommand : ICommand<EmployeeDto>
{
    public EmployeeDto Employee { get; set; } = default!;

}