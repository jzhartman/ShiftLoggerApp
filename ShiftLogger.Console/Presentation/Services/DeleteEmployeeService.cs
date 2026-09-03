using ShiftLogger.Application.Employees.Commands.DeleteEmployee;
using ShiftLogger.Console.ApiClients.Employees;
using ShiftLogger.Console.Presentation.Models;
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

    public async Task RunAsync(EmployeeViewModel employee)
    {
        var confirmDelete = UserInput.GetConfirmation($"Are you sure you want to delete [green]{employee.FirstName} {employee.LastName}[/]?");

        Messages.PrintBlankLines(1);
        if (confirmDelete)
        {
            var result = await _employeeApiClient.DeleteAsync(new DeleteEmployeeCommand(employee.Id, employee.FirstName, employee.LastName));

            if (result.IsSuccess)
                AnsiConsole.WriteLine($"Successfully deleted {employee.FirstName} {employee.LastName}");

            if (result.IsFailure)
                Messages.OutputErrorMessage(result.Errors);
        }
        else
        {
            Messages.Cancelled($"Did not delete [green]{employee.FirstName} {employee.LastName}[/]");
        }

        Messages.PrintBlankLines(1);
        Messages.PressAnyKeyToContinue();

        return;
    }
}
