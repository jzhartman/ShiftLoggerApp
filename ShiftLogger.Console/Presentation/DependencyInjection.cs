using Microsoft.Extensions.DependencyInjection;
using ShiftLogger.Console.Presentation.Services;
using ShiftLogger.Console.Presentation.Views;
using ShiftLogger.Console.Presentation.Views.Menus;

namespace ShiftLogger.Console.Presentation;

internal static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddTransient<MainMenuView>();
        services.AddTransient<EmployeeMenuView>();
        services.AddTransient<SelectEmployeeView>();

        services.AddTransient<SelectEmployeeService>();
        services.AddTransient<CreateEmployeeService>();
        services.AddTransient<UpdateEmployeeService>();
        services.AddTransient<DeleteEmployeeService>();

        services.AddTransient<CreateShiftService>();
        services.AddTransient<ViewShiftsService>();

        return services;
    }
}
