using ShiftLogger.Console.Presentation.Enums;
using ShiftLogger.Console.Presentation.Output;
using ShiftLogger.Console.Presentation.Services;
using ShiftLogger.Console.Presentation.Views.Menus;
using Spectre.Console;

namespace ShiftLogger.Console;

internal class App
{
    private readonly MainMenuView _mainMenu;
    private readonly SelectEmployeeService _selectEmployeeService;
    private readonly CreateEmployeeService _createEmployeeService;

    public App(MainMenuView mainMenu, SelectEmployeeService selectEmployeeService, CreateEmployeeService createEmployeeService)
    {
        _mainMenu = mainMenu;

        _selectEmployeeService = selectEmployeeService;
        _createEmployeeService = createEmployeeService;
    }

    public async Task RunAsync()
    {
        bool exitApp = false;
        MainMenuItem[] menuItems = Enum.GetValues<MainMenuItem>();

        while (exitApp == false)
        {
            AnsiConsole.Clear();
            var selection = _mainMenu.Render(menuItems);

            switch (selection)
            {
                case MainMenuItem.SelectEmployee:
                    await _selectEmployeeService.RunAsync();
                    break;
                case MainMenuItem.CreateEmployee:
                    await _createEmployeeService.RunAsync();
                    break;
                case MainMenuItem.Exit:
                    Messages.GoodbyeMessage();
                    exitApp = true;
                    break;
                default:
                    AnsiConsole.WriteLine("ERROR: Unknown input for main menu selection!");
                    break;
            }
        }
    }
}
