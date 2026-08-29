using ShiftLogger.Application.Employees.Dtos;
using Spectre.Console;

namespace ShiftLogger.Console.Presentation.Views;

internal class SelectEmployeeView
{
    public EmployeeDto Render(List<EmployeeDto> employees)
    {
        AnsiConsole.Clear();

        var selection = AnsiConsole.Prompt(
            new SelectionPrompt<EmployeeDto>()
            .Title("Select an employee from the list below:")
            .EnableSearch()
            .SearchPlaceholderText("Begin typing to search list...")
            .UseConverter(e => $"{e.LastName}, {e.FirstName}")
            .AddChoices(employees));

        return selection;
    }
}
