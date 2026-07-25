using ShiftLogger.Domain.Models;
using ShiftLogger.Infrastructure.Repositories;

namespace ShiftLogger.Application.Shifts.Commands.UpdateShift;

public class UpdateShiftHandler
{
    private readonly IShiftsRepository _shiftsRepository;

    public UpdateShiftHandler(IShiftsRepository shiftsRepository)
    {
        _shiftsRepository = shiftsRepository;
    }

    public async Task HandleAsync(UpdateShiftCommand shift)
    {
        await _shiftsRepository.UpdateShiftByIdAsync(new Shift
        {
            Id = shift.Id,
            EmployeeId = shift.EmployeeId,
            ClockInTime = shift.ClockInTime,
            ClockOutTime = shift.ClockInTime
        });

        await _shiftsRepository.SaveChangesAsync();
    }
}
