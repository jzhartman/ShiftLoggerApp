namespace ShiftLogger.Api.DTOs;

internal record EmployeeApiRequest(
    int id,
    string FirstName,
    string LastName);