using AutoMapper;
using JobFit.Application.Employees.Commands;
using JobFit.Application.Employees.Models;
using JobFit.Application.Employees.Services;
using JobFit.Domain.Common.Commands;
using JobFit.Domain.Entities;

namespace JobFit.Infrastructure.Employees.Commands;

public class CreateEmployeeCommandHandler(IMapper mapper, IEmployeeService employeeService) : ICommandHandler<CreateEmployeeCommand, EmployeeDto>
{
    public async Task<EmployeeDto> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = mapper.Map<Employee>(request.Employee);
        
        var createdEmployee = await employeeService.CreateAsync(employee,new CommandOptions(), cancellationToken);
        
        return mapper.Map<EmployeeDto>(createdEmployee);
    }
}