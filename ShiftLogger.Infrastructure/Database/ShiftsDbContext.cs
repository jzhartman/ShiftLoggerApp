using Microsoft.EntityFrameworkCore;
using ShiftLogger.Domain.Models;

namespace ShiftLogger.Infrastructure.Database;

public class ShiftsDbContext : DbContext
{
    public ShiftsDbContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Shift> Shifts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShiftsDbContext).Assembly);
    }

    public void SeedData()
    {
        Shifts.RemoveRange(Shifts);

        var employeeAShifts = new List<Shift>
        {
            new Shift
            {
                ClockInTime = DateTime.Now.AddDays(-3),
                ClockOutTime = DateTime.Now.AddDays(-3).AddHours(6),
                IsClockedIn = false,
                IsClockedOut = true,
                NeedsReviewed = false
            },
            new Shift
            {
                ClockInTime = DateTime.Now.AddDays(-5),
                ClockOutTime = DateTime.Now.AddDays(-5).AddHours(8),
                IsClockedIn = false,
                IsClockedOut = true,
                NeedsReviewed = false
            }
        };

        Users.RemoveRange(Users);

        Users.Add(
            new User
            {
                FirstName = "Jason",
                LastName = "Hartman",
                EmployeeId = "123456",
                IsActive = true,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                Shifts = employeeAShifts
            });

        SaveChanges();
    }

}
