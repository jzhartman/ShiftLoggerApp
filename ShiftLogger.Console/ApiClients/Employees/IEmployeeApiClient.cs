using ShiftLogger.Application.Employees.Commands.CreateEmployee;
using ShiftLogger.Application.Employees.Commands.DeleteEmployee;
using ShiftLogger.Application.Employees.Dtos;
using ShiftLogger.Domain.Validation;

namespace ShiftLogger.Console.ApiClients.Employees;

internal interface IEmployeeApiClient
{
    Task<Result> CreateAsync(CreateEmployeeCommand command);
    Task<Result> DeleteAsync(DeleteEmployeeCommand command);
    Task<Result<List<EmployeeDto>>> GetAllAsync();
    Task<Result> UpdateAsync(UpdateEmployeeCommand command);
}