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

}
