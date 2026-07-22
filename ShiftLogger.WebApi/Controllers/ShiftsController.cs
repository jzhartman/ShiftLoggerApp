using Microsoft.AspNetCore.Mvc;
using ShiftLogger.Api.DTOs;
using ShiftLogger.Domain.Models;

namespace ShiftLogger.Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ShiftsController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<ShiftLoggerApiResponse<Shift>> CreateShift(CreateShiftCommand shift)
    {

    }
}
