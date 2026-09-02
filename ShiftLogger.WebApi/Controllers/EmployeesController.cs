using Microsoft.AspNetCore.Mvc;
using ShiftLogger.Api.DTOs;
using ShiftLogger.Application.Employees.Commands.CreateEmployee;
using ShiftLogger.Application.Employees.Commands.DeleteEmployee;
using ShiftLogger.Application.Employees.Commands.UpdateEmployee;
using ShiftLogger.Application.Employees.Requests.GetAllEmployees;
using ShiftLogger.Application.Employees.Requests.GetEmployeeById;
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
        var result = await handler.HandleAsync(command);
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<ShiftLoggerApiResponse<List<Employee>>>> GetAllEmployeesAsync(
        [FromServices] GetAllEmpoyeesHandler handler)
    {
        var result = await handler.HandleAsync();
        return Ok(result);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult> GetEmployeeById(
        int id,
        [FromServices] GetEmployeeByIdHandler handler)
    {
        var result = await handler.HandleAsync(id);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateEmployeeAsync(
        int id,
        UpdateEmployeeCommand command,
        [FromServices] UpdateEmployeeHandler handler)
    {
        command = command with { Id = id };
        var result = await handler.HandleAsync(command);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteEmployeeAsync(
        int id,
        DeleteEmployeeCommand command,
        [FromServices] DeleteEmployeeHandler handler)
    {
        command = command with { Id = id };
        var result = await handler.HandleAsync(command);
        return Ok(result);
    }

}
