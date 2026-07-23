namespace ShiftLogger.Application.Shifts.Commands.CreateShift;

public record CreateShiftCommand(
    int EmployeeId,
    DateTime ClockInTime,
    DateTime ClockOutTime
    );