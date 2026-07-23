using Microsoft.EntityFrameworkCore;
using ShiftLogger.Domain.Models;

namespace ShiftLogger.Infrastructure.Database;

public class ShiftsDbContext : DbContext
{
    public ShiftsDbContext(DbContextOptions<ShiftsDbContext> options) : base(options)
    {
    }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Shift> Shifts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShiftsDbContext).Assembly);
    }

    public void SeedData()
    {
        Shifts.RemoveRange(Shifts);

        var employeeShifts = new List<Shift>
        {
            new Shift
            {
                ClockInTime = new DateTime(2026, 07, 06, 08, 00, 00),
                ClockOutTime = new DateTime(2026, 07, 06, 17, 00, 00)
            },
                        new Shift
            {
                ClockInTime = new DateTime(2026, 07, 07, 08, 00, 00),
                ClockOutTime = new DateTime(2026, 07, 07, 17, 00, 00)
            },
            new Shift
            {
                ClockInTime = new DateTime(2026, 07, 08, 08, 00, 00),
                ClockOutTime = new DateTime(2026, 07, 08, 17, 00, 00)
            },
            new Shift
            {
                ClockInTime = new DateTime(2026, 07, 09, 08, 00, 00),
                ClockOutTime = new DateTime(2026, 07, 09, 17, 00, 00)
            },
            new Shift
            {
                ClockInTime = new DateTime(2026, 07, 10, 08, 00, 00),
                ClockOutTime = new DateTime(2026, 07, 10, 17, 00, 00)
            },
                        new Shift
            {
                ClockInTime = new DateTime(2026, 07, 13, 08, 00, 00),
                ClockOutTime = new DateTime(2026, 07, 13, 17, 00, 00)
            },
                        new Shift
            {
                ClockInTime = new DateTime(2026, 07, 14, 08, 00, 00),
                ClockOutTime = new DateTime(2026, 07, 14, 17, 00, 00)
            },
            new Shift
            {
                ClockInTime = new DateTime(2026, 07, 15, 08, 00, 00),
                ClockOutTime = new DateTime(2026, 07, 15, 17, 00, 00)
            },
            new Shift
            {
                ClockInTime = new DateTime(2026, 07, 16, 08, 00, 00),
                ClockOutTime = new DateTime(2026, 07, 16, 17, 00, 00)
            },
            new Shift
            {
                ClockInTime = new DateTime(2026, 07, 17, 08, 00, 00),
                ClockOutTime = new DateTime(2026, 07, 17, 17, 00, 00)
            }
        };

        Employees.RemoveRange(Employees);

        Employees.Add(
            new Employee
            {
                FirstName = "Jason",
                LastName = "Hartman",
                Shifts = employeeShifts
            });

        SaveChanges();
    }

}
