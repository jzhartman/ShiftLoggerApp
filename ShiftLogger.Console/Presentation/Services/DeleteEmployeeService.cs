using ShiftLogger.Console.Presentation.Output;
using Spectre.Console;

namespace ShiftLogger.Console.Presentation.Services;

internal class DeleteEmployeeService
{
    public async Task RunAsync()
    {
        AnsiConsole.WriteLine("Deleting employee....");
        Messages.PressAnyKeyToContinue();

        return;
    }
}
