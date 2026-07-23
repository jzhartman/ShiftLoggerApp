using ShiftLogger.Application.Shifts.Dtos;
using ShiftLogger.Domain.Models;
using ShiftLogger.Infrastructure.Repositories;

namespace ShiftLogger.Application.Shifts.Requests.GetShiftsByEmployeeId;

public class GetShiftsByEmployeeIdHandler
{
    private readonly IShiftsRepository _shiftsRepository;

    public GetShiftsByEmployeeIdHandler(IShiftsRepository shiftsRepository)
    {
        _shiftsRepository = shiftsRepository;
    }

    public async Task<List<ShiftDto>> HandleAsync(GetShiftsQuery request, CancellationToken ct)
    {
        var shifts = await _shiftsRepository.GetAllShiftsByUserIdAsync(request.Id);

        return MapShifts(shifts);
    }

    private List<ShiftDto> MapShifts(List<Shift> shifts)
    {
        var output = new List<ShiftDto>();

        foreach (var shift in shifts)
        {
            output.Add(new ShiftDto(shift.Id, shift.EmployeeId, shift.ClockInTime, shift.ClockOutTime));
        }

        return output;
    }
}
