using ShiftLogger.Application.Shifts.Dtos;
using ShiftLogger.Domain.Models;
using ShiftLogger.Domain.Validation;
using ShiftLogger.Domain.Validation.Errors;
using ShiftLogger.Infrastructure.Repositories;

namespace ShiftLogger.Application.Shifts.Requests.GetShiftsByEmployeeId;

public class GetShiftsByEmployeeIdHandler
{
    private readonly IShiftsRepository _shiftsRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public GetShiftsByEmployeeIdHandler(IShiftsRepository shiftsRepository, IEmployeeRepository employeeRepository)
    {
        _shiftsRepository = shiftsRepository;
        _employeeRepository = employeeRepository;
    }

    public async Task<Result<List<ShiftDto>?>> HandleAsync(GetShiftsQuery request)
    {
        var employeeExistsResult = await _employeeRepository.EmployeeExistsByIdAsync(request.Id);
        if (!employeeExistsResult.Value)
            return Result<List<ShiftDto>?>.Failure(Errors.EmployeeNotFound);
        if (employeeExistsResult.IsFailure)
            return Result<List<ShiftDto>?>.Failure(employeeExistsResult.Errors);

        var shiftsResult = await _shiftsRepository.GetAllShiftsByUserIdAsync(request.Id);
        if (shiftsResult.IsFailure)
            return Result<List<ShiftDto>?>.Failure(shiftsResult.Errors);

        return Result<List<ShiftDto>?>.Success(MapShifts(shiftsResult.Value));
    }

    private List<ShiftDto> MapShifts(List<Shift>? shifts)
    {
        var output = new List<ShiftDto>();

        if (shifts is not null)
        {
            foreach (var shift in shifts)
            {
                output.Add(new ShiftDto(shift.Id, shift.EmployeeId, shift.ClockInTime, shift.ClockOutTime));
            }
        }

        return output;
    }
}
