using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ShiftLogger.Application;
using ShiftLogger.Console.ApiClients.DependencyInjection;
using ShiftLogger.Console.Configuration;
using ShiftLogger.Console.Presentation;

namespace ShiftLogger.Console;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddDebug();
            })
            .ConfigureServices((context, services) =>
            {
                services.Configure<ApiSettings>(context.Configuration.GetSection("ApiSettings"));
                services.AddConsoleUI();
                services.AddApplication();
                services.AddPresentation();
                services.AddTransient<App>();
            })
            .Build();

        await host.Services.GetRequiredService<App>().RunAsync();
    }

}