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
        var result = await _shiftsRepository.ShiftExistsById(command.Id);

        if (result is null || result.Value == false)
            return Result.Failure(Errors.ShiftNotFound);

        if (result.IsFailiure)
            return result;

        await _shiftsRepository.DeleteShiftAsync(new Shift
        {
            Id = command.Id,
            EmployeeId = command.EmployeeId,
            ClockInTime = command.ClockInTime,
            ClockOutTime = command.ClockOutTime
        });

        return Result.Success();
    }
}
