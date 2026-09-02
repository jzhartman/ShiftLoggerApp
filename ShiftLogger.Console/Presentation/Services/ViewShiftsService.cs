using ShiftLogger.Console.ApiClients.Shifts;
using ShiftLogger.Console.Presentation.Models;
using ShiftLogger.Console.Presentation.Output;
using Spectre.Console;

namespace ShiftLogger.Console.Presentation.Services;

internal class ViewShiftsService
{
    private readonly IShiftApiClient _shiftApiClient;

    public ViewShiftsService(IShiftApiClient shiftApiClient)
    {
        _shiftApiClient = shiftApiClient;
    }

    public async Task RunAsync(EmployeeViewModel employee)
    {




        AnsiConsole.WriteLine("Viewing shifts....");

        var result = await _shiftApiClient.GetByIdAsync(new(employee.Id, employee.FirstName, employee.LastName));

        if (result.IsSuccess)
        {
            if (result.Value.Count == 0)
            {
                AnsiConsole.WriteLine($"No shifts recorded for {employee.FirstName} {employee.LastName}");
            }

            else
            {
                AnsiConsole.WriteLine($"Id\tStart Time\tEnd Time");

                foreach (var shift in result.Value)
                {
                    AnsiConsole.WriteLine($"{shift.Id}\t{shift.ClockInTime}\t{shift.ClockOutTime}");
                }
                Messages.PressAnyKeyToContinue();
            }

            if (result.IsFailure)
                Messages.OutputErrorMessage(result.Errors);

            Messages.PressAnyKeyToContinue();

            return;
        }
    }
}