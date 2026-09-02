using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ShiftLogger.Console.ApiClients.Employees;
using ShiftLogger.Console.ApiClients.Shifts;
using ShiftLogger.Console.Configuration;

namespace ShiftLogger.Console.ApiClients.DependencyInjection;

public static class ApiClientRegistration
{
    public static IServiceCollection AddConsoleUI(this IServiceCollection services)
    {
        services.AddHttpClient<IEmployeeApiClient, EmployeeApiClient>((sp, client) =>
        {
            var settings = sp.GetRequiredService<IOptions<ApiSettings>>().Value;
            client.BaseAddress = new Uri(settings.BaseUrl + "employees/");
        });

        services.AddHttpClient<IShiftApiClient, ShiftApiClient>((sp, client) =>
        {
            var settings = sp.GetRequiredService<IOptions<ApiSettings>>().Value;
            client.BaseAddress = new Uri(settings.BaseUrl + "shifts/");
        });

        return services;
    }
}
