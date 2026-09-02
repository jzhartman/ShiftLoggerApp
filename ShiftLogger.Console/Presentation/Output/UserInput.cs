using Spectre.Console;

namespace ShiftLogger.Console.Presentation.Output;

internal static class UserInput
{
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
        var datePrompt = new TextPrompt<DateTime>(message)
            .AllowEmpty()
            .Validate(input =>
            {
                if (input < DateTime.MinValue)
                    return ValidationResult.Error("[red]Invalid Data:[/] Date must be in this millenium.");
                if (input > DateTime.Now)
                    return ValidationResult.Error("[red]Invalid Data:[/] Cannot enter a future time.");

                return ValidationResult.Success();
            });

        return AnsiConsole.Prompt(datePrompt);
    }

    internal static bool GetRetryConfirmation(string message)
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
