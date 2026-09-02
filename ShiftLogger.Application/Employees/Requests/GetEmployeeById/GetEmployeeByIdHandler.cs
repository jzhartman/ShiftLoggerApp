using ShiftLogger.Application.Employees.Dtos;
using ShiftLogger.Domain.Validation;
using ShiftLogger.Domain.Validation.Errors;
using ShiftLogger.Infrastructure.Repositories;

namespace ShiftLogger.Application.Employees.Requests.GetEmployeeById;

public class GetEmployeeByIdHandler
{
    private readonly IEmployeeRepository _employeeRepository;

    public GetEmployeeByIdHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<Result<EmployeeDto>> HandleAsync(int id)
    {
        var employeesResult = await _employeeRepository.GetEmployeeByIdAsync(id);
        if (employeesResult.IsFailure)
            return Result<EmployeeDto>.Failure(employeesResult.Errors);

        if (employeesResult.Value is null || employeesResult.Value.Id == 0 ||
            employeesResult.Value.FirstName is null || employeesResult.Value.LastName is null)
            return Result<EmployeeDto>.Failure(Errors.QueryReturnedNull);

        return Result<EmployeeDto>.Success(new EmployeeDto(employeesResult.Value.Id,
            employeesResult.Value.FirstName,
            employeesResult.Value.LastName)
            );
    }
}
