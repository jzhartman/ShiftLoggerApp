using Microsoft.AspNetCore.Mvc;
using ShiftLogger.Api.DTOs;
using ShiftLogger.Application.Shifts.Commands.CreateShift;
using ShiftLogger.Application.Shifts.Requests.GetShiftsByEmployeeId;
using ShiftLogger.Domain.Models;

namespace ShiftLogger.Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ShiftsController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> CreateShift(
        CreateShiftCommand command,
        [FromServices] CreateShiftHandler handler)
    {
        await handler.HandleAsync(command);

        return Ok();
    }

    [HttpGet("{employeeId}")]
    public async Task<ActionResult<ShiftLoggerApiResponse<List<Shift>>>> GetShiftsByEmployeeId(
        int employeeId,
        [FromServices] GetShiftsByEmployeeIdHandler handler)
    {
        var result = await handler.HandleAsync(new GetShiftsQuery(employeeId), HttpContext.RequestAborted);
        return Ok(result);
    }

}
