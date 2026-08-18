using ShiftLogger.Domain.Models;
using ShiftLogger.Domain.Validation;
using ShiftLogger.Domain.Validation.Errors;
using ShiftLogger.Infrastructure.Repositories;

namespace ShiftLogger.Application.Shifts.Commands.DeleteShift;

public class DeleteShiftHandler
{
    private readonly IShiftsRepository _shiftsRepository;

    public DeleteShiftHandler(IShiftsRepository shiftsRepository)
    {
        _shiftsRepository = shiftsRepository;
    }

    public async Task<Result> HandleAsync(DeleteShiftCommand command)
    {
        var shiftToDelete = new Shift
        {
            Id = command.Id,
            EmployeeId = command.EmployeeId,
            ClockInTime = command.ClockInTime,
            ClockOutTime = command.ClockOutTime
        };

        var shiftExistsResult = await _shiftsRepository.ShiftExistsByIdAsync(shiftToDelete.Id);
        if (!shiftExistsResult.Value)
            return Result.Failure(Errors.ShiftIdNotFound);
        if (shiftExistsResult.IsFailure)
            return Result.Failure(shiftExistsResult.Errors);

        if (shiftExistsResult.IsFailure)
            return Result.Failure(shiftExistsResult.Errors);

        return await _shiftsRepository.DeleteShiftAsync(shiftToDelete);
    }
}
