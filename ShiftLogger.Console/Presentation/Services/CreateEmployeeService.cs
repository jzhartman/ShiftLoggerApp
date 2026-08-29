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
        AnsiConsole.WriteLine("Creating employee....");

        AnsiConsole.Write("First  Name: ");
        var firstName = System.Console.ReadLine();

        AnsiConsole.Write("Last  Name: ");
        var lastName = System.Console.ReadLine();

        var command = new CreateEmployeeCommand(firstName, lastName);

        var result = await _employeeApiClient.Create(command);


        Messages.PressAnyKeyToContinue();

        return;
    }
}
