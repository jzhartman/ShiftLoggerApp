using ShiftLogger.Domain.Models;
using ShiftLogger.Infrastructure.Repositories;

namespace ShiftLogger.Application.Shifts.Commands.CreateShift;

public class CreateShiftHandler
{
    private readonly IShiftsRepository _shiftsRepository;

    public CreateShiftHandler(IShiftsRepository shiftsRepository)
    {
        _shiftsRepository = shiftsRepository;
    }

    public async Task HandleAsync(CreateShiftCommand command)
    {
        await _shiftsRepository.CreateShiftAsync(new Shift
        {
            UserId = command.UserId,
            ClockInTime = command.ClockInTime,
            ClockOutTime = command.ClockOutTime,
            IsClockedIn = command.IsClockedIn,
            IsClockedOut = command.IsClockedOut,
            NeedsReviewed = command.NeedsReviewed,
        });

        await _shiftsRepository.SaveChangesAsync();
    }
}
