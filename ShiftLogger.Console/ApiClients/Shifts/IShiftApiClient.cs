using ShiftLogger.Application.Employees.Dtos;
using ShiftLogger.Application.Shifts.Commands.CreateShift;
using ShiftLogger.Application.Shifts.Dtos;
using ShiftLogger.Domain.Validation;

namespace ShiftLogger.Console.ApiClients.Shifts;

internal interface IShiftApiClient
{
    Task<Result> CreateAsync(CreateShiftCommand shift);
    Task<Result<List<ShiftDto>>> GetByIdAsync(EmployeeDto employee);
}