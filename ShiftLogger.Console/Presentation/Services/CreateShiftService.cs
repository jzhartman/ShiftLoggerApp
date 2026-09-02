using ShiftLogger.Application.Shifts.Commands.CreateShift;
using ShiftLogger.Console.ApiClients.Shifts;
using ShiftLogger.Console.Presentation.Models;
using ShiftLogger.Console.Presentation.Output;
using Spectre.Console;
using System.Globalization;

namespace ShiftLogger.Console.Presentation.Services;

internal class CreateShiftService
{
    private readonly IShiftApiClient _shiftApiClient;

    public CreateShiftService(IShiftApiClient shiftApiClient)
    {
        _shiftApiClient = shiftApiClient;
    }

    public async Task RunAsync(EmployeeViewModel employee)
    {
        AnsiConsole.Clear();
        AnsiConsole.Write($"Enter new shift for {employee.FirstName} {employee.LastName}");
        Messages.PrintBlankLines(2);

        AnsiConsole.Write("Start Time: ");
        var startTime = DateTime.ParseExact(System.Console.ReadLine(), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

        AnsiConsole.Write("End Time: ");
        var endTime = DateTime.ParseExact(System.Console.ReadLine(), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

        var command = new CreateShiftCommand(employee.Id, startTime, endTime);

        var result = await _shiftApiClient.CreateAsync(command);

        if (result.IsSuccess)
            AnsiConsole.WriteLine($"Successfully added shift.");

        if (result.IsFailure)
            Messages.OutputErrorMessage(result.Errors);

        Messages.PressAnyKeyToContinue();

        return;
    }
}
