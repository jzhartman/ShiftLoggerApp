using ShiftLogger.Application.Employees.Dtos;
using ShiftLogger.Console.ApiClients.Employees;
using ShiftLogger.Console.Presentation.Enums;
using ShiftLogger.Console.Presentation.Models;
using ShiftLogger.Console.Presentation.Output;
using ShiftLogger.Console.Presentation.Views;
using ShiftLogger.Console.Presentation.Views.Menus;
using ShiftLogger.Domain.Validation;
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
                returnToMainMenu = await EmployeeMenuSelection(result);
        }
        return;
    }


    private async Task<bool> EmployeeMenuSelection(Result<List<EmployeeDto>> result)
    {
        var employeeSelection = _selectEmployeeView.Render(result.Value);

        var employee = new EmployeeViewModel
        {
            Id = employeeSelection.Id,
            FirstName = employeeSelection.FirstName,
            LastName = employeeSelection.LastName
        };

        bool returnToEmployeeSelection = false;

        while (returnToEmployeeSelection == false)
        {
            var menuSelection = _employeeMenu.Render(Enum.GetValues<EmployeeMenuItem>().ToArray());

            switch (menuSelection)
            {
                case EmployeeMenuItem.LogShift:
                    await _createShiftService.RunAsync(employee);
                    break;
                case EmployeeMenuItem.ViewShifts:
                    await _viewShiftsService.RunAsync(employee);
                    break;
                case EmployeeMenuItem.UpdateEmployee:
                    var employeeUpdated = await _updateEmployeeService.RunAsync(employee);
                    if (employeeUpdated) await UpdateCurrentEmployee(employee);
                    break;
                case EmployeeMenuItem.DeleteEmployee:
                    await _deleteEmployeeService.RunAsync(employee);
                    returnToEmployeeSelection = true;
                    break;
                case EmployeeMenuItem.ReturnToEmployeeSelection:
                    returnToEmployeeSelection = true;
                    break;
                case EmployeeMenuItem.ReturnToMainMenu:
                    returnToEmployeeSelection = true;
                    return true;
                default:
                    AnsiConsole.WriteLine("ERROR: Unknown input for main menu selection!");
                    break;
            }
        }
        return false;
    }

    private async Task UpdateCurrentEmployee(EmployeeViewModel employee)
    {
        var updatedEmployeeResponse = await _employeeApiClient.GetByIdAsync(employee.Id);

        if (updatedEmployeeResponse.Value is not null)
        {
            employee.FirstName = updatedEmployeeResponse.Value.FirstName;
            employee.LastName = updatedEmployeeResponse.Value.LastName;
        }
    }
}
