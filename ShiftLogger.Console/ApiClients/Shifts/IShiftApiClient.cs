using ShiftLogger.Application.Employees.Dtos;
using ShiftLogger.Application.Shifts.Commands.CreateShift;
using ShiftLogger.Application.Shifts.Commands.DeleteShift;
using ShiftLogger.Application.Shifts.Commands.UpdateShift;
using ShiftLogger.Application.Shifts.Dtos;
using ShiftLogger.Domain.Validation;

namespace ShiftLogger.Console.ApiClients.Shifts;

internal interface IShiftApiClient
{
    Task<Result> CreateAsync(CreateShiftCommand shift);
    Task<Result> DeleteAsync(DeleteShiftCommand command);
    Task<Result<List<ShiftDto>>> GetByDateRangeAndIdAsync(EmployeeDto employee, DateTime startDate, DateTime endDate);
    Task<Result<List<ShiftDto>>> GetByIdAsync(EmployeeDto employee);
    Task<Result> UpdateAsync(UpdateShiftCommand command);
}