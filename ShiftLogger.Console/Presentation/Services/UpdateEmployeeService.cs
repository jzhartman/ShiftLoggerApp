using ShiftLogger.Console.Presentation.Output;
using Spectre.Console;

namespace ShiftLogger.Console.Presentation.Services;

internal class UpdateEmployeeService
{
    public async Task RunAsync()
    {
        AnsiConsole.WriteLine("Updating employee....");
        Messages.PressAnyKeyToContinue();

        return;
    }
}
