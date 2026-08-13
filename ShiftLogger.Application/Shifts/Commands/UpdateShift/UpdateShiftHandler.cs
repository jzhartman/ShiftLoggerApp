using ShiftLogger.Domain.Models;
using ShiftLogger.Domain.Validation;
using ShiftLogger.Domain.Validation.Errors;
using ShiftLogger.Infrastructure.Repositories;

namespace ShiftLogger.Application.Shifts.Commands.UpdateShift;

public class UpdateShiftHandler
{
    private readonly IShiftsRepository _shiftsRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public UpdateShiftHandler(IShiftsRepository shiftsRepository, IEmployeeRepository employeeRepository)
    {
        _shiftsRepository = shiftsRepository;
        _employeeRepository = employeeRepository;
    }

    public async Task<Result> HandleAsync(UpdateShiftCommand shift)
    {
        var updatedShift = new Shift
        {
            Id = shift.Id,
            EmployeeId = shift.EmployeeId,
            ClockInTime = shift.ClockInTime,
            ClockOutTime = shift.ClockInTime
        };

        if (!(await _shiftsRepository.ShiftExistsById(updatedShift.Id)).Value)
            return Result.Failure(Errors.ShiftIdNotFound);

        var employeeExistsResult = await _employeeRepository.EmployeeExistsById(updatedShift.EmployeeId);
        if (!employeeExistsResult.Value)
            return Result.Failure(employeeExistsResult.Errors);

        if (updatedShift.ClockInTime > updatedShift.ClockOutTime)
            return Result.Failure(Errors.ClockInTimePrecedesClockOutTime);

        if ((await _shiftsRepository.OverlapsExistingShiftsExcludingCurrent(updatedShift)).Value)
            return Result.Failure(Errors.NewShiftOverlapsExistingShift);

        var updateResult = await _shiftsRepository.UpdateShiftByIdAsync(updatedShift);

        if (updateResult.IsFailure)
            return updateResult;

        return await _shiftsRepository.SaveChangesAsync();
    }
}
