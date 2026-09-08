using ShiftLogger.Console.ApiClients.Shifts;

namespace ShiftLogger.Console.Presentation.Services;

internal class DeleteShiftService
{
    private readonly IShiftApiClient _shiftApiClient;

    public DeleteShiftService(IShiftApiClient shiftApiClient)
    {
        _shiftApiClient = shiftApiClient;
    }

    public async Task RunAsync()
    {

    }
}
