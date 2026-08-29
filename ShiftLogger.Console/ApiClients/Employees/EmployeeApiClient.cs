using ShiftLogger.Application.Employees.Commands.CreateEmployee;
using ShiftLogger.Application.Employees.Commands.DeleteEmployee;
using ShiftLogger.Application.Employees.Dtos;
using ShiftLogger.Console.ApiClients.Responses;
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
        try
        {
            var response = await _http.GetAsync("employees");

            if (!response.IsSuccessStatusCode)
                return Result<List<EmployeeDto>>.Failure(await ReadErrorsAsync(response));

            var deserializedResponse = await response.Content.ReadFromJsonAsync<ApiResponse<List<EmployeeDto>>>();

            if (deserializedResponse is null)
                return Result<List<EmployeeDto>>.Failure(Errors.DeserializationError);

            if (deserializedResponse.IsFailure)
                return Result<List<EmployeeDto>>.Failure(deserializedResponse.Errors);

            return Result<List<EmployeeDto>>.Success(deserializedResponse.Value ?? new List<EmployeeDto>());
        }
        catch (Exception ex)
        {
            return Result<List<EmployeeDto>>.Failure(new Error("ApiError", ex.Message));
        }
    }

    public async Task<Result> CreateAsync(CreateEmployeeCommand command)
    {
        try
        {
            var response = await _http.PostAsJsonAsync("employees", command);

            if (!response.IsSuccessStatusCode)
                return Result.Failure(await ReadErrorsAsync(response));

            var deserializedResponse = await response.Content.ReadFromJsonAsync<ApiResponse<object>>();

            if (deserializedResponse is null)
                return Result.Failure(Errors.DeserializationError);

            if (deserializedResponse.IsFailure)
                return Result.Failure(deserializedResponse.Errors);

            return Result.Success();

        }
        catch (Exception ex)
        {
            return Result<List<EmployeeDto>>.Failure(new Error("ApiError", ex.Message));
        }
    }
    public async Task<Result> UpdateAsync(UpdateEmployeeCommand command)
    {
        try
        {
            var response = await _http.PutAsJsonAsync($"employees/{command.Id}", command);

            if (!response.IsSuccessStatusCode)
                return Result.Failure(await ReadErrorsAsync(response));

            var deserializedResponse = await response.Content.ReadFromJsonAsync<ApiResponse<object>>();

            if (deserializedResponse is null)
                return Result.Failure(Errors.DeserializationError);

            if (deserializedResponse.IsFailure)
                return Result.Failure(deserializedResponse.Errors);

            return Result.Success();

        }
        catch (Exception ex)
        {
            return Result<List<EmployeeDto>>.Failure(new Error("ApiError", ex.Message));
        }
    }
    public async Task<Result> DeleteAsync(DeleteEmployeeCommand command)
    {
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"employees/{command.Id}")
            {
                Content = JsonContent.Create(command)
            };

            var response = await _http.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                return Result.Failure(await ReadErrorsAsync(response));

            var deserializedResponse = await response.Content.ReadFromJsonAsync<ApiResponse<object>>();

            if (deserializedResponse is null)
                return Result.Failure(Errors.DeserializationError);

            if (deserializedResponse.IsFailure)
                return Result.Failure(deserializedResponse.Errors);

            return Result.Success();

        }
        catch (Exception ex)
        {
            return Result<List<EmployeeDto>>.Failure(new Error("ApiError", ex.Message));
        }
    }

    private async Task<List<Error>> ReadErrorsAsync(HttpResponseMessage response)
    {
        var result = await response.Content.ReadFromJsonAsync<Result>();

        if (result?.Errors is not null && result.Errors.Count > 0)
            return result.Errors;

        return new List<Error> { new Error("HttpError", $"Status code: {response.StatusCode}") };
    }
}
