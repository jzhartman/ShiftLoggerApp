using ShiftLogger.Application.Employees.Dtos;
using ShiftLogger.Domain.Validation;

namespace ShiftLogger.Console.ApiClients.Employees;

internal interface IEmployeeApiClient
{
    Task<Result<List<EmployeeDto>>> GetAllAsync();
}