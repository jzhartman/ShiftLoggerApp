using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShiftLogger.Infrastructure.Database;
using ShiftLogger.Infrastructure.Repositories;

namespace ShiftLogger.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastrucutre(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ShiftsDbContext>(opt => opt.UseSqlite(connectionString));

        services.AddTransient<IShiftsRepository, ShiftsRepository>();

        return services;
    }
}
