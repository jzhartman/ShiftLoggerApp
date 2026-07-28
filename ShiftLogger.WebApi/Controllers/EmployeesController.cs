using Microsoft.AspNetCore.Mvc;
using ShiftLogger.Api.DTOs;
using ShiftLogger.Application.Employees.Commands.CreateEmployee;
using ShiftLogger.Application.Employees.Commands.DeleteEmployee;
using ShiftLogger.Application.Employees.Commands.UpdateEmployee;
using ShiftLogger.Application.Employees.Requests.GetAllEmployees;
using ShiftLogger.Domain.Models;

namespace ShiftLogger.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> CreateEmployeeAsync(
        CreateEmployeeCommand command,
        [FromServices] CreateEmployeeHandler handler)
    {
        await handler.HandleAsync(command);
        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<ShiftLoggerApiResponse<List<Employee>>>> GetAllEmployeesAsync(
        [FromServices] GetAllEmpoyeesHandler handler)
    {
        var result = await handler.HandleAsync();

        return Ok(result);
    }

    [HttpPut("{command.Id}")]
    public async Task<ActionResult> UpdateEmployeeAsync(
        UpdateEmployeeCommand command,
        [FromServices] UpdateEmployeeHandler handler)
    {
        await handler.HandleAsync(command);

        return Ok();
    }

    [HttpDelete("{command.Id}")]
    public async Task<ActionResult> DeleteEmployeeAsync(
        DeleteEmployeeCommand command,
        [FromServices] DeleteEmployeeHandler handler)
    {
        await handler.HandleAsync(command);

        return Ok();
    }

}
