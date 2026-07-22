using Microsoft.Extensions.DependencyInjection;
using ShiftLogger.Application.Shifts.Commands.CreateShift;

namespace ShiftLogger.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateShiftHandler>();

        return services;
    }
}
