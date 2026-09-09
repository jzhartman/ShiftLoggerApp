using ShiftLogger.Console.Presentation.Enums;
using Spectre.Console;

namespace ShiftLogger.Console.Presentation.Views.Menus;

internal class ShiftMenuView
{
    public ShiftMenuItem Render(ShiftMenuItem[] menuItems)
    {
        var selection = AnsiConsole.Prompt(
            new SelectionPrompt<ShiftMenuItem>()
                .UseConverter(m => m switch
                {
                    ShiftMenuItem.EditShift => "Edit Shift Time",
                    ShiftMenuItem.DeleteShift => "Delete Shift",
                    ShiftMenuItem.SelectNewDateRange => "Select New Date Range",
                    ShiftMenuItem.Return => "Return to Last Menu",
                    _ => m.ToString()
                })
                .AddChoices(menuItems));

        return selection;
    }
}
