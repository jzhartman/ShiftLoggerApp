using ShiftLogger.Application.Employees.Dtos;
using ShiftLogger.Domain.Models;
using ShiftLogger.Domain.Validation;
using ShiftLogger.Infrastructure.Repositories;

namespace ShiftLogger.Application.Employees.Requests.GetAllEmployees;

public class GetAllEmpoyeesHandler
{
    private readonly IEmployeeRepository _employeeRepository;

    public GetAllEmpoyeesHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<Result<List<EmployeeDto>>> HandleAsync()
    {
        var employeesResult = await _employeeRepository.GetAllAsync();
        if (employeesResult.IsFailure)
            return Result<List<EmployeeDto>>.Failure(employeesResult.Errors);

        return Result<List<EmployeeDto>>.Success(MapToDto(employeesResult.Value));
    }

    private List<EmployeeDto> MapToDto(List<Employee>? employees)
    {
        var output = new List<EmployeeDto>();

        if (employees is not null)
        {
            foreach (var employee in employees)
            {
                output.Add(new EmployeeDto(
                employee.Id,
                employee.FirstName,
                employee.LastName));
            }
        }

        return output;
    }
}
