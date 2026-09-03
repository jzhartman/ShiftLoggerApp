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

    internal static DateTime GetTimeFromUser(string message)
    {
        var dateString = AnsiConsole.Prompt(
            new TextPrompt<string>(message)
            .Validate(input =>
            {
                if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
                    return ValidationResult.Success();

                bool isValid = DateTime.TryParseExact(
                    input, _timeFormat,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out _);

                return isValid
                    ? ValidationResult.Success()
                    : ValidationResult.Error($"[red]Invalid format![/] Please use [yellow]{_timeFormat}[/].");
            }));

        return DateTime.ParseExact(dateString, _timeFormat, CultureInfo.InvariantCulture);
    }

    internal static bool GetConfirmation(string message)
    {
        return AnsiConsole.Confirm(message);
    }

    //internal bool GetEditContactConfirmationFromUser(FullContactViewModel originalContact, EditContactViewModel newContact)
    //{
    //    string preamble = $"Confirm the following changes for the contact {originalContact.FirstName} {originalContact.LastName}:";
    //    string changes = string.Empty;

    //    if (newContact.ChangedFirstName) changes += $"\t[yellow]{originalContact.FirstName}[/] to [green]{newContact.FirstName}[/]\r\n";
    //    if (newContact.ChangedLastName) changes += $"\t[yellow]{originalContact.LastName}[/] to [green]{newContact.LastName}[/]\r\n";
    //    if (newContact.ChangedPhoneNumber) changes += $"\t[yellow]{originalContact.PhoneNumber}[/] to [green]{newContact.PhoneNumber}[/]\r\n";
    //    if (newContact.ChangedEmail) changes += $"\t[yellow]{originalContact.Email}[/] to [green]{newContact.Email}[/]\r\n";
    //    if (newContact.ChangedCategory) changes += $"\t[yellow]{originalContact.CategoryName}[/] to [green]{newContact.CategoryName}[/]\r\n";

    //    return AnsiConsole.Confirm($"{preamble}\r\n\r\n{changes}\r\nConfirm:");
    //}
}
