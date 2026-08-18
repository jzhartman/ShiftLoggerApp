using ShiftLogger.Application.Employees.Commands.DeleteEmployee;
using ShiftLogger.Domain.Models;
using ShiftLogger.Domain.Validation;
using ShiftLogger.Domain.Validation.Errors;
using ShiftLogger.Infrastructure.Repositories;

namespace ShiftLogger.Application.Employees.Commands.UpdateEmployee;

public class UpdateEmployeeHandler
{
    private readonly IEmployeeRepository _employeeRepository;

    public UpdateEmployeeHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<Result> HandleAsync(UpdateEmployeeCommand command)
    {
        var updatedEmployee = new Employee
        {
            Id = command.Id,
            FirstName = command.FirstName,
            LastName = command.LastName
        };

        if (string.IsNullOrWhiteSpace(command.FirstName) || string.IsNullOrWhiteSpace(command.LastName))
            return Result.Failure(Errors.EmployeeNameIsBlank);

        var employeeExistsByIdResult = await _employeeRepository.EmployeeExistsByIdAsync(updatedEmployee.Id);
        if (!employeeExistsByIdResult.Value)
            return Result.Failure(Errors.EmployeeNotFound);
        if (employeeExistsByIdResult.IsFailure)
            return Result.Failure(employeeExistsByIdResult.Errors);

        var employeeExistsByFullNameResult = await _employeeRepository.EmployeeExistsByFullNameAsync(updatedEmployee);
        if (employeeExistsByFullNameResult.Value)
            return Result.Failure(Errors.NoChangesToUpdatedData);
        if (employeeExistsByFullNameResult.IsFailure)
            return Result.Failure(employeeExistsByFullNameResult.Errors);

        var updateResult = await _employeeRepository.UpdateEmployeeAsync(updatedEmployee);
        if (updateResult.IsFailure)
            return Result.Failure(updateResult.Errors);

        return await _employeeRepository.SaveChangesAsync();
    }
}
