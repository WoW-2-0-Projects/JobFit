using JobFit.Application.Employees.Models;
using JobFit.Domain.Common.Commands;
using Microsoft.AspNetCore.Http;

namespace JobFit.Application.Employees.Commands;

public class CreateEmployeeCommand : ICommand<EmployeeDto>
{
    public EmployeeDto Employee { get; set; } = default!;
    
    public IFormFile File { get; set; } = default!;
}