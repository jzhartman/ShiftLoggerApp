namespace ShiftLogger.Domain.Validation.Errors;

public static class Errors
{
    public static readonly Error None = Error.None;
    public static readonly Error GenericNull = new("GenericNull", "Cannot be null");



    public static readonly Error ShiftNotFound = new("SR-404", "The requested shift could not be found.");

}
