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
        var employeeUpdated = false;

        AnsiConsole.Clear();
        AnsiConsole.MarkupLine($"Updating Employee Record for [DeepSkyBlue1]{employee.FirstName} {employee.LastName}[/]");
        Messages.PrintBlankLines(2);

        var newFirstName = UserInput.GetNameFromUser("Enter new [yellow]first name[/]: ");
        var newLastName = UserInput.GetNameFromUser("Enter new [yellow]last name[/]: ");

        Messages.PrintBlankLines(1);
        var confirmUpdate = UserInput.GetConfirmation($"Confirm changing [yellow]{employee.FirstName} {employee.LastName}[/] to [green]{newFirstName} {newLastName}[/]?");

        Messages.PrintBlankLines(1);
        if (confirmUpdate == true)
        {
            var command = new UpdateEmployeeCommand(employee.Id, newFirstName, newLastName);

            var result = await _employeeApiClient.UpdateAsync(command);

            if (result.IsSuccess)
            {
                Messages.Success($"Updated [yellow]{employee.FirstName} {employee.LastName}[/] to [green]{newFirstName} {newLastName}[/]");
                employeeUpdated = true;
            }

            if (result.IsFailure)
                Messages.OutputErrorMessage(result.Errors);
        }
        else
        {
            Messages.Cancelled($"Did not update [yellow]{employee.FirstName} {employee.LastName}[/]");
        }

        Messages.PrintBlankLines(1);
        Messages.PressAnyKeyToContinue();

        return employeeUpdated;
    }
}
