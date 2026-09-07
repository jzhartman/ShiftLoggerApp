using ShiftLogger.Console.ApiClients.Shifts;
using ShiftLogger.Console.Presentation.Enums;
using ShiftLogger.Console.Presentation.Models;
using ShiftLogger.Console.Presentation.Output;
using ShiftLogger.Console.Presentation.Views;
using ShiftLogger.Console.Presentation.Views.Menus;
using Spectre.Console;

namespace ShiftLogger.Console.Presentation.Services;

internal class ViewShiftsService
{
    private readonly IShiftApiClient _shiftApiClient;
    private readonly ShiftTableView _shiftTableView;
    private readonly ShiftMenuView _shiftMenuView;

    public ViewShiftsService(IShiftApiClient shiftApiClient, ShiftTableView shiftTableView, ShiftMenuView shiftMenuView)
    {
        _shiftApiClient = shiftApiClient;
        _shiftTableView = shiftTableView;
        _shiftMenuView = shiftMenuView;
    }

    public async Task RunAsync(EmployeeViewModel employee)
    {
        bool returnToEmployeeMenu = false;

        while (returnToEmployeeMenu == false)
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine($"Viewing shifts for [DeepSkyBlue1]{employee.FirstName} {employee.LastName}[/]");
            Messages.PrintBlankLines(2);


            var startDate = UserInput.GetTimeFromUser("Enter start date (Format: [yellow]yyyy-MM-dd[/]): ", true);
            var endDate = UserInput.GetTimeFromUser("Enter end date (Format: [yellow]yyyy-MM-dd[/]): ", true);
            endDate = endDate.AddHours(23).AddMinutes(59).AddSeconds(59);


            var result = await _shiftApiClient.GetByDateRangeAndIdAsync(new(employee.Id, employee.FirstName, employee.LastName),
                                                                        startDate,
                                                                        endDate);

            if (result.IsSuccess)
            {
                _shiftTableView.Render(result.Value);

                var menuSelection = _shiftMenuView.Render(Enum.GetValues<ShiftMenuItem>().ToArray());

                switch (menuSelection)
                {
                    case ShiftMenuItem.EditShift:
                        await UpdateShift();
                        break;
                    case ShiftMenuItem.DeleteShift:
                        await DeleteShift();
                        break;
                    case ShiftMenuItem.Return:
                        break;
                    default:
                        AnsiConsole.MarkupLine("[red]ERROR:[/] Unknown input for main menu selection!");
                        break;
                }

                if (result.IsFailure)
                    Messages.OutputErrorMessage(result.Errors);

                returnToEmployeeMenu = !UserInput.GetConfirmation("Select a new date range?");
            }

            return;
        }
    }

    private async Task UpdateShift()
    {

    }

    private async Task DeleteShift()
    {

    }
}