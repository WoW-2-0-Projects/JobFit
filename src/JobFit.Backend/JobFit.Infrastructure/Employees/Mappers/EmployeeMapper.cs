using AutoMapper;
using JobFit.Application.Employees.Models;
using JobFit.Domain.Entities;

namespace JobFit.Infrastructure.Employees.Mappers;

public class EmployeeMapper : Profile
{
    public EmployeeMapper()
    {
        CreateMap<EmployeeDto, Employee>().ReverseMap();
    }
}