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
        AnsiConsole.Clear();
        AnsiConsole.Write($"Updating Employee Record for {employee.FirstName} {employee.LastName}");
        Messages.PrintBlankLines(2);

        var newFirstName = UserInput.GetNameFromUser("Enter new first name: ");
        var newLastName = UserInput.GetNameFromUser("Enter new last name: ");

        var confirmUpdate = UserInput.GetConfirmation($"Confirm changing {employee.FirstName} {employee.LastName} to {newFirstName} {newLastName}?");

        if (confirmUpdate == true)
        {
            var command = new UpdateEmployeeCommand(employee.Id, newFirstName, newLastName);

            var result = await _employeeApiClient.UpdateAsync(command);

            if (result.IsSuccess)
            {
                AnsiConsole.WriteLine($"Successfully updated {employee.FirstName} {employee.LastName} to {newFirstName} {newLastName}");
                Messages.PressAnyKeyToContinue();
                return true;
            }

            if (result.IsFailure)
                Messages.OutputErrorMessage(result.Errors);
        }
        else
        {
            AnsiConsole.WriteLine("Update cancelled.");
        }

        Messages.PressAnyKeyToContinue();

        return false;
    }
}
