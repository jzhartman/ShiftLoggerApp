namespace ShiftLogger.Domain.Validation.Errors;

public static class Errors
{
    //ToDo: Make all error codes unique by appending a final digit or character to the end of duplicates

    public static readonly Error None = Error.None;
    public static readonly Error GenericNull = new("GenericNull", "Cannot be null");

    public static readonly Error NoSaveData = new("SR-409", "Saved failed because no changes were detected.");
    public static readonly Error SaveFailed = new("SR-500", "Save failed for unknown reason.");

    public static readonly Error ShiftIdNotFound = new("SR-404", "The requested shift could not be found.");
    public static readonly Error ShiftsNotFoundForEmployeeId = new("SR-404", "No shifts found for the specified employee.");
    public static readonly Error ShiftAlreadyExists = new("SR-409", "A shift with the requested data already exists.");


    public static readonly Error EmployeeNotFound = new("ER-404", "The requested employee could not be found.");




    public static readonly Error ClockInTimePrecedesClockOutTime = new("SH-409", "The Clock In time cannot be on or after the Clock Out time.");
    public static readonly Error ShiftOverlapReturnedNull = new("SH-404", "Shift overlap check returned null.");
    public static readonly Error NewShiftOverlapsExistingShift = new("SH-409", "This shift cannot overlap an existing shift.");

}
