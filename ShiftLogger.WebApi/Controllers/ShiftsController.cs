using Microsoft.AspNetCore.Mvc;
using ShiftLogger.Api.DTOs;
using ShiftLogger.Application.Shifts.Commands.CreateShift;
using ShiftLogger.Application.Shifts.Commands.DeleteShift;
using ShiftLogger.Application.Shifts.Commands.UpdateShift;
using ShiftLogger.Application.Shifts.Requests.GetShiftsByEmployeeId;
using ShiftLogger.Domain.Models;

namespace ShiftLogger.Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ShiftsController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> CreateShiftAsync(
        CreateShiftCommand command,
        [FromServices] CreateShiftHandler handler)
    {
        var result = await handler.HandleAsync(command);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ShiftLoggerApiResponse<List<Shift>>>> GetShiftsByEmployeeIdAsync(
        int id,
        [FromServices] GetShiftsByEmployeeIdHandler handler)
    {
        var result = await handler.HandleAsync(new GetShiftsQuery(id));
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateShiftAsync(
        int id,
        UpdateShiftCommand command,
        [FromServices] UpdateShiftHandler handler)
    {
        command = command with { Id = id };
        var result = await handler.HandleAsync(command);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteShiftAsync(
        int id,
        DeleteShiftCommand command,
        [FromServices] DeleteShiftHandler handler)
    {
        command = command with { Id = id };
        var result = await handler.HandleAsync(command);
        return Ok(result);
    }
}