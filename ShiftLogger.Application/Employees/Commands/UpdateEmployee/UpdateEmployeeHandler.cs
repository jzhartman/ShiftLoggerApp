using ShiftLogger.Application.Employees.Commands.DeleteEmployee;
using ShiftLogger.Domain.Models;
using ShiftLogger.Infrastructure.Repositories;

namespace ShiftLogger.Application.Employees.Commands.UpdateEmployee;

public class UpdateEmployeeHandler
{
    private readonly IEmployeeRepository _employeeRepository;

    public UpdateEmployeeHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task HandleAsync(UpdateEmployeeCommand command)
    {
        await _employeeRepository.UpdateEmployeeAsync(new Employee
        {
            Id = command.Id,
            FirstName = command.FirstName,
            LastName = command.LastName
        });

        await _employeeRepository.SaveChangesAsync();
    }
}
