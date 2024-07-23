using JobFit.Application.Employees.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JobFit.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class EmployeeController(IMediator mediator) : ControllerBase
{
    
    [HttpPost]
    public async ValueTask<IActionResult> CreateEmployee([FromBody]CreateEmployeeCommand command,CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command,cancellationToken);
        return Ok(result);
    }
}