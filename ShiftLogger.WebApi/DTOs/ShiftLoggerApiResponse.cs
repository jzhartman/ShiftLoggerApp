using System.Net;

namespace ShiftLogger.Api.DTOs;

public class ShiftLoggerApiResponse<T>
{
    public bool RequestFailed { get; set; } = false;
    public HttpStatusCode ResponseCode { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;
    public T? Data { get; set; }
    //public int TotalCount { get; set; }
    //public int CurrentPage { get; set; }
    //public int PageSize { get; set; }
    //public bool HasNext { get; set; }
    //public bool HasPrevious { get; set; }

}
