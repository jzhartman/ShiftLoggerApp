using Microsoft.Extensions.DependencyInjection;
using ShiftLogger.Application.Shifts.Commands.CreateShift;
using ShiftLogger.Application.Shifts.Requests.GetShiftsByEmployeeId;

namespace ShiftLogger.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateShiftHandler>();
        services.AddScoped<GetShiftsByEmployeeIdHandler>();

        return services;
    }
}
