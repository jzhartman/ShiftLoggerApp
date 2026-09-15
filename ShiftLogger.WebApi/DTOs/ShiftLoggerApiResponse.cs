using System.Net;

namespace ShiftLogger.Api.DTOs;

public class ShiftLoggerApiResponse<T>
{
    public bool RequestFailed { get; set; } = false;
    public HttpStatusCode ResponseCode { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;
    public T? Data { get; set; }
}