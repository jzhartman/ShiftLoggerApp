namespace ShiftLogger.Application.Shifts.Dtos;

public record ShiftDto(int Id, int EmployeeId, DateTime ClockInTime, DateTime ClockOutTime);