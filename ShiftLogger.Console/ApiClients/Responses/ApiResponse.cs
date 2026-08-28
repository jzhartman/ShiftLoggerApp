using ShiftLogger.Domain.Validation.Errors;

namespace ShiftLogger.Console.ApiClients.Responses;

internal class ApiResponse<T>
{
    public T? Value { get; set; }
    public bool IsSuccess { get; set; }
    public bool IsFailure => !IsSuccess;
    public List<Error> Errors { get; set; } = new();
}
