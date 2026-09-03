using ShiftLogger.Application.Employees.Commands.CreateEmployee;
using ShiftLogger.Console.ApiClients.Employees;
using ShiftLogger.Console.Presentation.Output;
using Spectre.Console;

namespace ShiftLogger.Console.Presentation.Services;

internal class CreateEmployeeService
{
    private readonly IEmployeeApiClient _employeeApiClient;

    public CreateEmployeeService(IEmployeeApiClient employeeApiClient)
    {
        _employeeApiClient = employeeApiClient;
    }

    public async Task RunAsync()
    {
        bool returnToMainMenu = false;

        while (returnToMainMenu == false)
        {
            AnsiConsole.Clear();
            AnsiConsole.Write("Create New Employee");
            Messages.PrintBlankLines(2);

            var firstName = UserInput.GetNameFromUser("Enter [yellow]first name[/] of employee:");
            var lastName = UserInput.GetNameFromUser("Enter [yellow]last name[/] of employee:");

            Messages.PrintBlankLines(1);
            var confirmAdd = UserInput.GetConfirmation($"Confirm adding [green]{firstName} {lastName}[/]?");

            if (confirmAdd == true)
            {
                var command = new CreateEmployeeCommand(firstName, lastName);

                var result = await _employeeApiClient.CreateAsync(command);

                if (result.IsSuccess)
                {
                    Messages.Success($"Added {command.FirstName} {command.LastName}");
                }

                if (result.IsFailure)
                {
                    Messages.OutputErrorMessage(result.Errors);
                }
            }
            else
            {
                Messages.Cancelled($"Did not add [yellow]{firstName} {lastName}[/] to employee list");
            }

            Messages.PrintBlankLines(1);
            returnToMainMenu = !(UserInput.GetConfirmation("Enter another employee?"));
        }

        Messages.PrintBlankLines(1);
        Messages.PressAnyKeyToContinue();
        return;
    }
}
