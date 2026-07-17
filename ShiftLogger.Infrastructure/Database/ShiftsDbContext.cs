using Microsoft.EntityFrameworkCore;

namespace ShiftLogger.Infrastructure.Database;

public class ShiftsDbContext : DbContext
{
    public ShiftsDbContext(DbContextOptions options) : base(options)
    {

    }
}
