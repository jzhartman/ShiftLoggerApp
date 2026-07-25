namespace ShiftLogger.Application.Shifts.Commands.DeleteShift;

public record DeleteShiftCommand(int Id, int EmployeeId, DateTime ClockInTime, DateTime ClockOutTime);