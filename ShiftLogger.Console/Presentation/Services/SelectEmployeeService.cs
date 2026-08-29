using ShiftLogger.Console.ApiClients.Employees;
using ShiftLogger.Console.Presentation.Enums;
using ShiftLogger.Console.Presentation.Output;
using ShiftLogger.Console.Presentation.Views;
using ShiftLogger.Console.Presentation.Views.Menus;
using Spectre.Console;

namespace ShiftLogger.Console.Presentation.Services;

internal class SelectEmployeeService
{
    private readonly IEmployeeApiClient _employeeApiClient;
    private readonly SelectEmployeeView _selectEmployeeView;
    private readonly CreateShiftService _createShiftService;
    private readonly ViewShiftsService _viewShiftsService;
    private readonly UpdateEmployeeService _updateEmployeeService;
    private readonly DeleteEmployeeService _deleteEmployeeService;
    private readonly EmployeeMenuView _employeeMenu;

    public SelectEmployeeService(IEmployeeApiClient employeeApiClient, SelectEmployeeView selectEmployeeView, EmployeeMenuView employeeMenu,
                                    CreateShiftService createShiftService, ViewShiftsService viewShiftsService,
                                    UpdateEmployeeService updateEmployeeService, DeleteEmployeeService deleteEmployeeService)
    {
        _employeeApiClient = employeeApiClient;
        _selectEmployeeView = selectEmployeeView;
        _employeeMenu = employeeMenu;
        _createShiftService = createShiftService;
        _viewShiftsService = viewShiftsService;
        _updateEmployeeService = updateEmployeeService;
        _deleteEmployeeService = deleteEmployeeService;
    }

    public async Task RunAsync()
    {
        bool returnToMainMenu = false;

        while (returnToMainMenu == false)
        {
            var result = await _employeeApiClient.GetAllAsync();

            if (result.IsFailure)
            {
                Messages.OutputErrorMessage(result.Errors);
                returnToMainMenu = true;
                continue;
            }

            if (result.Value is null || result.Value.Count == 0)
            {
                AnsiConsole.WriteLine("No Employees Found");
                Messages.PressAnyKeyToContinue();
                returnToMainMenu = true;
                continue;
            }

            if (result.IsSuccess)
            {
                var employeeSelection = _selectEmployeeView.Render(result.Value);

                bool returnToEmployeeSelection = false;

                while (returnToEmployeeSelection == false)
                {
                    var menuSelection = _employeeMenu.Render(Enum.GetValues<EmployeeMenuItem>().ToArray());

                    switch (menuSelection)
                    {
                        case EmployeeMenuItem.LogShift:
                            await _createShiftService.RunAsync();
                            break;
                        case EmployeeMenuItem.ViewShifts:
                            await _viewShiftsService.RunAsync();
                            break;
                        case EmployeeMenuItem.UpdateEmployee:
                            await _updateEmployeeService.RunAsync(employeeSelection);
                            break;
                        case EmployeeMenuItem.DeleteEmployee:
                            await _deleteEmployeeService.RunAsync();
                            break;
                        case EmployeeMenuItem.ReturnToEmployeeSelection:
                            returnToEmployeeSelection = true;
                            break;
                        case EmployeeMenuItem.ReturnToMainMenu:
                            returnToEmployeeSelection = true;
                            returnToMainMenu = true;
                            break;
                        default:
                            AnsiConsole.WriteLine("ERROR: Unknown input for main menu selection!");
                            break;
                    }
                }
            }
        }
        return;
    }
}
