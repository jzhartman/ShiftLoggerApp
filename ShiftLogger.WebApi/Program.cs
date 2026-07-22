using ShiftLogger.Infrastructure;
using ShiftLogger.Infrastructure.Database;

namespace ShiftLogger.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var connectionString = builder.Configuration.GetConnectionString("Default");


        builder.Services.AddControllers();
        builder.Services.AddInfrastrucutre(connectionString);

        //builder.Services.AddTransient<CreateShiftHandler>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ShiftsDbContext>();
            context.SeedData();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
