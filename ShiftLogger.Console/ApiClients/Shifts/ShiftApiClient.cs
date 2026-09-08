using ShiftLogger.Application.Employees.Dtos;
using ShiftLogger.Application.Shifts.Commands.CreateShift;
using ShiftLogger.Application.Shifts.Commands.DeleteShift;
using ShiftLogger.Application.Shifts.Commands.UpdateShift;
using ShiftLogger.Application.Shifts.Dtos;
using ShiftLogger.Console.ApiClients.Responses;
using ShiftLogger.Domain.Validation;
using ShiftLogger.Domain.Validation.Errors;
using System.Net.Http.Json;

namespace ShiftLogger.Console.ApiClients.Shifts;

internal class ShiftApiClient : IShiftApiClient
{
    private readonly HttpClient _http;

    public ShiftApiClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<Result> CreateAsync(CreateShiftCommand command)
    {
        try
        {
            var response = await _http.PostAsJsonAsync("", command);

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
    public async Task<Result<List<ShiftDto>>> GetByIdAsync(EmployeeDto employee)
    {
        try
        {
            var response = await _http.GetAsync($"{employee.Id}");

            if (!response.IsSuccessStatusCode)
                return Result<List<ShiftDto>>.Failure(await ReadErrorsAsync(response));

            var deserializedResponse = await response.Content.ReadFromJsonAsync<ApiResponse<List<ShiftDto>>>();

            if (deserializedResponse is null)
                return Result<List<ShiftDto>>.Failure(Errors.DeserializationError);

            if (deserializedResponse.IsFailure)
                return Result<List<ShiftDto>>.Failure(deserializedResponse.Errors);

            if (deserializedResponse.Value is null)
                deserializedResponse.Value = new List<ShiftDto>();

            return Result<List<ShiftDto>>.Success(deserializedResponse.Value);
        }
        catch (Exception ex)
        {
            return Result<List<ShiftDto>>.Failure(new Error("ApiError", ex.Message));
        }
    }
    public async Task<Result<List<ShiftDto>>> GetByDateRangeAndIdAsync(EmployeeDto employee, DateTime startDate, DateTime endDate)
    {
        try
        {
            var response = await _http.GetAsync($"{employee.Id}/range?startDate={startDate:O}&endDate={endDate:O}");

            if (!response.IsSuccessStatusCode)
                return Result<List<ShiftDto>>.Failure(await ReadErrorsAsync(response));

            var deserializedResponse = await response.Content.ReadFromJsonAsync<ApiResponse<List<ShiftDto>>>();

            if (deserializedResponse is null)
                return Result<List<ShiftDto>>.Failure(Errors.DeserializationError);

            if (deserializedResponse.IsFailure)
                return Result<List<ShiftDto>>.Failure(deserializedResponse.Errors);

            if (deserializedResponse.Value is null)
                deserializedResponse.Value = new List<ShiftDto>();

            return Result<List<ShiftDto>>.Success(deserializedResponse.Value);
        }
        catch (Exception ex)
        {
            return Result<List<ShiftDto>>.Failure(new Error("ApiError", ex.Message));
        }
    }

    public async Task<Result> UpdateAsync(UpdateShiftCommand command)
    {
        try
        {
            var response = await _http.PutAsJsonAsync($"{command.Id}", command);

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
    public async Task<Result> DeleteAsync(DeleteShiftCommand command)
    {
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"{command.Id}")
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
