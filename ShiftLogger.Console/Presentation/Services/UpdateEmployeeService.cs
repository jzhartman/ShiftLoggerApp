using ShiftLogger.Application.Employees.Commands.DeleteEmployee;
using ShiftLogger.Console.ApiClients.Employees;
using ShiftLogger.Console.Presentation.Models;
using ShiftLogger.Console.Presentation.Output;
using Spectre.Console;

namespace ShiftLogger.Console.Presentation.Services;

internal class UpdateEmployeeService
{
    private readonly IEmployeeApiClient _employeeApiClient;

    public UpdateEmployeeService(IEmployeeApiClient employeeApiClient)
    {
        _employeeApiClient = employeeApiClient;
    }

    public async Task<bool> RunAsync(EmployeeViewModel employee)
    {
        AnsiConsole.WriteLine("Updating employee....");

        AnsiConsole.Write("First  Name: ");
        var newFirstName = System.Console.ReadLine();

        AnsiConsole.Write("Last  Name: ");
        var newLastName = System.Console.ReadLine();

        var command = new UpdateEmployeeCommand(employee.Id, newFirstName ?? "", newLastName ?? "");

        var result = await _employeeApiClient.UpdateAsync(command);

        if (result.IsSuccess)
        {
            AnsiConsole.WriteLine($"Successfully updated {employee.FirstName} {employee.LastName} to {newFirstName} {newLastName}");
            Messages.PressAnyKeyToContinue();
            return true;
        }

        if (result.IsFailure)
            Messages.OutputErrorMessage(result.Errors);

        Messages.PressAnyKeyToContinue();

        return false;
    }
}
