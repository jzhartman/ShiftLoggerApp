namespace ShiftLogger.Api.DTOs;

internal record UserApiRequest(
    string FirstName,
    string LastName,
    string EmployeeId,
    bool IsActive,
    DateTime DateCreated,
    DateTime DateModified);