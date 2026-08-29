using ShiftLogger.Application.Employees.Commands.DeleteEmployee;
using ShiftLogger.Application.Employees.Dtos;
using ShiftLogger.Console.ApiClients.Employees;
using ShiftLogger.Console.Presentation.Output;
using Spectre.Console;

namespace ShiftLogger.Console.Presentation.Services;

internal class DeleteEmployeeService
{
    private readonly IEmployeeApiClient _employeeApiClient;

    public DeleteEmployeeService(IEmployeeApiClient employeeApiClient)
    {
        _employeeApiClient = employeeApiClient;
    }

    public async Task RunAsync(EmployeeDto employee)
    {
        AnsiConsole.WriteLine("Deleting employee....");

        var result = await _employeeApiClient.DeleteAsync(new DeleteEmployeeCommand(employee.Id, employee.FirstName, employee.LastName));

        if (result.IsSuccess)
            AnsiConsole.WriteLine($"Successfully deleted {employee.FirstName} {employee.LastName}");

        if (result.IsFailure)
            Messages.OutputErrorMessage(result.Errors);

        Messages.PressAnyKeyToContinue();

        return;
    }
}
