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
        AnsiConsole.Clear();
        AnsiConsole.Write("Create New Employee");
        Messages.PrintBlankLines(2);

        bool returnToMainMenu = false;

        while (returnToMainMenu == false)
        {
            bool employeeValid = false;

            var firstName = UserInput.GetNameFromUser("Enter first name of employee:");
            var lastName = UserInput.GetNameFromUser("Enter last name of employee:");

            var confirmAdd = UserInput.GetConfirmation($"Confirm adding {firstName} {lastName}?");

            if (confirmAdd == true)
            {
                var command = new CreateEmployeeCommand(firstName, lastName);

                var result = await _employeeApiClient.CreateAsync(command);

                if (result.IsSuccess)
                {
                    AnsiConsole.WriteLine($"Successfully added {command.FirstName} {command.LastName}");
                    employeeValid = true;
                    returnToMainMenu = true;
                }

                if (result.IsFailure)
                {
                    employeeValid = false;
                    Messages.OutputErrorMessage(result.Errors);
                }
            }

            if (employeeValid == false)
                returnToMainMenu = (UserInput.GetConfirmation("Retry entering name?")) ? false : true;
        }

        Messages.PressAnyKeyToContinue();

        return;
    }
}
