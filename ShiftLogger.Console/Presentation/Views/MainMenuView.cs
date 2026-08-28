using ShiftLogger.Console.Presentation.Enums;
using Spectre.Console;

namespace ShiftLogger.Console.Presentation.Views;

internal class MainMenuView
{
    public MainMenuItem Render(MainMenuItem[] menuItems)
    {
        AnsiConsole.Clear();
        var selection = AnsiConsole.Prompt(
            new SelectionPrompt<MainMenuItem>()
                .Title("Select a menu option:")
                .UseConverter(m => m switch
                {
                    MainMenuItem.SelectEmployee => "Select Employee",
                    MainMenuItem.CreateEmployee => "Create a New Employee",
                    MainMenuItem.Exit => "Close Application",
                    _ => m.ToString()
                })
                .AddChoices(menuItems));

        return selection;
    }
}
