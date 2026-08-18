using ShiftLogger.Domain.Models;
using ShiftLogger.Domain.Validation;
using ShiftLogger.Domain.Validation.Errors;
using ShiftLogger.Infrastructure.Repositories;

namespace ShiftLogger.Application.Employees.Commands.CreateEmployee;

public class CreateEmployeeHandler
{
    private readonly IEmployeeRepository _employeeRepository;

    public CreateEmployeeHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<Result> HandleAsync(CreateEmployeeCommand command)
    {
        var newEmployee = new Employee
        {
            FirstName = command.FirstName,
            LastName = command.LastName
        };

        if (string.IsNullOrWhiteSpace(command.FirstName) || string.IsNullOrWhiteSpace(command.LastName))
            return Result.Failure(Errors.EmployeeNameIsBlank);

        var employeeExistsResult = await _employeeRepository.EmployeeExistsByFullNameAsync(newEmployee);
        if (employeeExistsResult.Value)
            return Result.Failure(Errors.EmployeeAlreadyExists);
        if (employeeExistsResult.IsFailure)
            return Result.Failure(employeeExistsResult.Errors);

        var createResult = await _employeeRepository.CreateEmployeeAsync(newEmployee);
        if (createResult.IsFailure)
            return Result.Failure(createResult.Errors);

        return await _employeeRepository.SaveChangesAsync();
    }
}
