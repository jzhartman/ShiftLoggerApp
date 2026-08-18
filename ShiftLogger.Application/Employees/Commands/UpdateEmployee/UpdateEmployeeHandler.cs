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

        var employeeExistsByIdResult = await _employeeRepository.EmployeeExistsByIdAsync(updatedEmployee.Id);
        if (!employeeExistsByIdResult.Value)
            return Result.Failure(Errors.EmployeeNotFound);

        var employeeExistsByFullNameResult = await _employeeRepository.EmployeeExistsByFullNameAsync(updatedEmployee);
        if (employeeExistsByFullNameResult.Value)
            return Result.Failure(Errors.NoChangesToUpdatedData);

        await _employeeRepository.UpdateEmployeeAsync(updatedEmployee);

        await _employeeRepository.SaveChangesAsync();
    }
}
