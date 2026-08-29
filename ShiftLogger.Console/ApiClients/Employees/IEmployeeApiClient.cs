using ShiftLogger.Application.Employees.Commands.CreateEmployee;
using ShiftLogger.Application.Employees.Dtos;
using ShiftLogger.Domain.Validation;

namespace ShiftLogger.Console.ApiClients.Employees;

internal interface IEmployeeApiClient
{
    Task<Result> Create(CreateEmployeeCommand command);
    Task<Result<List<EmployeeDto>>> GetAllAsync();
}