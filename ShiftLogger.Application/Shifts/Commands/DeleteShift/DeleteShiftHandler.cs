using ShiftLogger.Domain.Models;
using ShiftLogger.Infrastructure.Repositories;

namespace ShiftLogger.Application.Shifts.Commands.DeleteShift;

public class DeleteShiftHandler
{
    private readonly IShiftsRepository _shiftsRepository;

    public DeleteShiftHandler(IShiftsRepository shiftsRepository)
    {
        _shiftsRepository = shiftsRepository;
    }

    public async Task HandleAsync(DeleteShiftCommand command)
    {
        await _shiftsRepository.DeleteShiftAsync(new Shift
        {
            Id = command.Id,
            EmployeeId = command.EmployeeId,
            ClockInTime = command.ClockInTime,
            ClockOutTime = command.ClockOutTime
        });

        await _shiftsRepository.SaveChangesAsync();
    }
}
