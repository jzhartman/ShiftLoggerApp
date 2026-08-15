using ShiftLogger.Application.Employees.Commands.DeleteEmployee;
using ShiftLogger.Domain.Models;
using ShiftLogger.Domain.Validation;
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
            return Result.Failure(employeeExistsByIdResult.Errors);



        await _employeeRepository.UpdateEmployeeAsync(updatedEmployee);

        await _employeeRepository.SaveChangesAsync();
    }
}
