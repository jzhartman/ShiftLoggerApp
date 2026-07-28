using ShiftLogger.Domain.Models;
using ShiftLogger.Infrastructure.Repositories;

namespace ShiftLogger.Application.Employees.Commands.CreateEmployee;

public class CreateEmployeeHandler
{
    private readonly IEmployeeRepository _employeeRepository;

    public CreateEmployeeHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task HandleAsync(CreateEmployeeCommand command)
    {
        await _employeeRepository.CreateEmployeeAsync(new Employee { FirstName = command.FirstName, LastName = command.LastName });

        await _employeeRepository.SaveChangesAsync();
    }
}
