using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShiftLogger.Infrastructure.Database;

namespace ShiftLogger.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastrucutre(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ShiftsDbContext>(opt => opt.UseSqlite(connectionString));

        return services;
    }
}
