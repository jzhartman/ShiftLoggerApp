using Spectre.Console;
using System.Globalization;

namespace ShiftLogger.Console.Presentation.Output;

internal static class UserInput
{
    private static readonly string _timeFormat = "yyyy-MM-dd HH:mm:ss";

    internal static string GetNameFromUser(string message)
    {
        var namePrompt = new TextPrompt<string>(message)
            .AllowEmpty()
            .Validate(input =>
            {
                if (string.IsNullOrWhiteSpace(input))
                    return ValidationResult.Error("[red]Required Field:[/] Name cannot be empty.");

                return ValidationResult.Success();
            });

        return AnsiConsole.Prompt(namePrompt);
    }
    internal static int GetNumberFromUser(string message, int maximum)
    {
        var numberPrompt = new TextPrompt<int>(message)
            .Validate(input =>
            {
                if (input < 1)
                    return ValidationResult.Error("[red]ERROR:[/] Value cannot be less than one.");
                if (input > maximum)
                    return ValidationResult.Error($"[red]ERROR:[/] Value cannot exceed {maximum}.");

                return ValidationResult.Success();
            });

        return AnsiConsole.Prompt(numberPrompt);
    }
    internal static DateTime GetTimeFromUser(string message, bool dateOnly = false)
    {
        var timeFormatToUse = _timeFormat;

        if (dateOnly) timeFormatToUse = "yyyy-MM-dd";

        var dateString = AnsiConsole.Prompt(
            new TextPrompt<string>(message)
            .Validate(input =>
            {
                if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
                    return ValidationResult.Success();

                bool isValid = DateTime.TryParseExact(
                    input, timeFormatToUse,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out _);

                return isValid
                    ? ValidationResult.Success()
                    : ValidationResult.Error($"[red]Invalid format![/] Please use [yellow]{timeFormatToUse}[/].");
            }));

        return DateTime.ParseExact(dateString, timeFormatToUse, CultureInfo.InvariantCulture);
    }
    internal static DateTime GetUpdatedTimeFromUser(string message)
    {
        var dateString = AnsiConsole.Prompt(
            new TextPrompt<string>(message)
            .AllowEmpty()
            .Validate(input =>
            {
                bool isValid = DateTime.TryParseExact(
                    input, _timeFormat,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out _);

                if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
                    isValid = true;

                return isValid
                    ? ValidationResult.Success()
                    : ValidationResult.Error($"[red]Invalid format![/] Please use [yellow]{_timeFormat}[/].");
            }));

        if (string.IsNullOrEmpty(dateString) || string.IsNullOrWhiteSpace(dateString))
            return DateTime.MinValue;

        return DateTime.ParseExact(dateString, _timeFormat, CultureInfo.InvariantCulture);
    }
    internal static bool GetConfirmation(string message)
    {
        return AnsiConsole.Confirm(message);
    }
}