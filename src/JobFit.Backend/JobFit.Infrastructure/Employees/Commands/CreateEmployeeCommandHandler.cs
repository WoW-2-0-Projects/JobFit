using AutoMapper;
using JobFit.Application.Common.FileStorage.Services;
using JobFit.Application.Employees.Commands;
using JobFit.Application.Employees.Models;
using JobFit.Application.Employees.Services;
using JobFit.Domain.Common.Commands;
using JobFit.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace JobFit.Infrastructure.Employees.Commands;

public class CreateEmployeeCommandHandler(
    IMapper mapper,
    IEmployeeService employeeService,
    IStorageFileProcessingService storageFileProcessingService
) : ICommandHandler<CreateEmployeeCommand, EmployeeDto>
{
    public async Task<EmployeeDto> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = mapper.Map<Employee>(request.Employee);

        var createdEmployee = await employeeService.CreateAsync(employee, new CommandOptions(), cancellationToken);

        /*if (request.File is null) throw new InvalidDataException("Data not found");

        if (httpContextAccessor.HttpContext is null || !httpContextAccessor.HttpContext.Request.HasFormContentType)
            throw new InvalidOperationException("Request does not contain a form content type");

        if (httpContextAccessor.HttpContext.Request.Form.Files.Count == 0)
            throw new InvalidOperationException("Request does not contain any files");*/

        // Get request user ID and open file stream to upload
        var file = request.File;
        var ownerId = createdEmployee.Id;
        await using var stream = file.OpenReadStream();
        
        var storageFile = await storageFileProcessingService.CreateAsync(
            stream,
            file.FileName,
            file.ContentType,
            ownerId,
            cancellationToken);


        return mapper.Map<EmployeeDto>(createdEmployee);
    }
}