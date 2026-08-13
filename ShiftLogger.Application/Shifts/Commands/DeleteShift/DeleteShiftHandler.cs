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

        var result = await _shiftsRepository.ShiftExistsById(shiftToDelete.Id);

        if (result is null || result.Value == false)
            return Result.Failure(Errors.ShiftIdNotFound);

        if (result.IsFailure)
            return result;

        return await _shiftsRepository.DeleteShiftAsync(shiftToDelete);
    }
}
