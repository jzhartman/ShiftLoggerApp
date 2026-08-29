using ShiftLogger.Console.Presentation.Output;
using Spectre.Console;

namespace ShiftLogger.Console.Presentation.Services;

internal class CreateShiftService
{
    public async Task RunAsync()
    {
        AnsiConsole.WriteLine("Creating shift....");
        Messages.PressAnyKeyToContinue();

        return;
    }
}
