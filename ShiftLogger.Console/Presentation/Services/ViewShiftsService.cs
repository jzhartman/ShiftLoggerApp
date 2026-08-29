using ShiftLogger.Console.Presentation.Output;
using Spectre.Console;

namespace ShiftLogger.Console.Presentation.Services;

internal class ViewShiftsService
{
    public async Task RunAsync()
    {
        AnsiConsole.WriteLine("Viewing shifts....");
        Messages.PressAnyKeyToContinue();

        return;
    }
}
