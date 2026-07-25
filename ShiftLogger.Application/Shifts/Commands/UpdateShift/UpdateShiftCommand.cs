namespace ShiftLogger.Application.Shifts.Commands.UpdateShift;

public record UpdateShiftCommand(int Id, int EmployeeId, DateTime ClockInTime, DateTime ClockOutTime);