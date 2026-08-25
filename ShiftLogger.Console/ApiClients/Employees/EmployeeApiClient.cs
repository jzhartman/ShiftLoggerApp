using ShiftLogger.Application.Employees.Dtos;
using ShiftLogger.Domain.Validation;
using ShiftLogger.Domain.Validation.Errors;
using System.Net.Http.Json;


namespace ShiftLogger.Console.ApiClients.Employees;

internal class EmployeeApiClient : IEmployeeApiClient
{
    private readonly HttpClient _http;

    public EmployeeApiClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<Result<List<EmployeeDto>>> GetAllAsync()
    {
        var apiResponse = await _http.GetAsync("employees");

        if (!apiResponse.IsSuccessStatusCode)
            return Result<List<EmployeeDto>>.Failure(await ReadErrorsAsync(apiResponse));

        var result = await apiResponse.Content.ReadFromJsonAsync<Result<List<EmployeeDto>>>();

        if (result is null)
            return Result<List<EmployeeDto>>.Failure(new Error("DeserializationError", "Could not parse API response."));

        if (result.IsFailure)
            return Result<List<EmployeeDto>>.Failure(result.Errors);

        return Result<List<EmployeeDto>>.Success(result.Value ?? new List<EmployeeDto>());
    }

    private async Task<List<Error>> ReadErrorsAsync(HttpResponseMessage response)
    {
        var result = await response.Content.ReadFromJsonAsync<Result>();

        if (result?.Errors is not null && result.Errors.Count > 0)
            return result.Errors;

        return new List<Error> { new Error("HttpError", $"Status code: {response.StatusCode}") };
    }
}
