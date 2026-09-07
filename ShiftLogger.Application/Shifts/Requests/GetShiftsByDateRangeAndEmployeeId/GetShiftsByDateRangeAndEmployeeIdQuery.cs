namespace ShiftLogger.Application.Shifts.Requests.GetShiftsByDateRangeAndEmployeeId;

public record GetShiftsByDateRangeAndEmployeeIdQuery(int EmployeeId, DateTime StartDate, DateTime EndDate);