using ShiftLogger.Console.Presentation.Enums;
using Spectre.Console;

namespace ShiftLogger.Console.Presentation.Views.Menus;

internal class EmployeeMenuView
{
    public EmployeeMenuItem Render(EmployeeMenuItem[] menuItems)
    {
        AnsiConsole.Clear();
        var selection = AnsiConsole.Prompt(
            new SelectionPrompt<EmployeeMenuItem>()
                .Title("Select a menu option:")
                .UseConverter(m => m switch
                {
                    EmployeeMenuItem.LogShift => "Log a New Shift",
                    EmployeeMenuItem.ViewShifts => "View Previous Shifts",
                    EmployeeMenuItem.UpdateEmployee => "Update Employee Name",
                    EmployeeMenuItem.DeleteEmployee => "Delete Employee",
                    EmployeeMenuItem.ReturnToEmployeeSelection => "Return to Employee Selection",
                    EmployeeMenuItem.ReturnToMainMenu => "Return to Main Menu",
                    _ => m.ToString()
                })
                .AddChoices(menuItems));

        return selection;
    }
}
