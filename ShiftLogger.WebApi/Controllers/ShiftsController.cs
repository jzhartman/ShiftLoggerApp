using Microsoft.AspNetCore.Mvc;
using ShiftLogger.Api.DTOs;
using ShiftLogger.Application.Shifts.Commands.CreateShift;
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

    [HttpGet("{userId}")]
    public async Task<ActionResult<ShiftLoggerApiResponse<List<Shift>>>> GetShiftsByUserId(int userId)
    {
        return Ok();
    }

}
