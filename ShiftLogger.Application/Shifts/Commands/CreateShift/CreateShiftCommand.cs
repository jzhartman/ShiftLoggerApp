namespace ShiftLogger.Application.Shifts.Commands.CreateShift;

public record CreateShiftCommand(
    int UserId,
    DateTime ClockInTime,
    DateTime ClockOutTime,
    bool IsClockedIn,
    bool IsClockedOut,
    bool NeedsReviewed
    );