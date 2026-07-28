namespace ShiftLogger.Application.Employees.Commands.DeleteEmployee;

public record DeleteEmployeeCommand(int Id, string FirstName, string LastName);