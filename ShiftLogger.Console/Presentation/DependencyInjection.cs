using Microsoft.Extensions.DependencyInjection;
using ShiftLogger.Console.Presentation.Services;
using ShiftLogger.Console.Presentation.Views;

namespace ShiftLogger.Console.Presentation;

internal static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddTransient<MainMenuView>();
        services.AddTransient<EmployeeMenuView>();

        services.AddTransient<SelectEmployeeService>();

        return services;
    }
}
