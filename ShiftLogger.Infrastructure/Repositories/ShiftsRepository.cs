using ShiftLogger.Domain.Models;
using ShiftLogger.Infrastructure.Database;

namespace ShiftLogger.Infrastructure.Repositories;

public class ShiftsRepository : IShiftsRepository
{
    private readonly ShiftsDbContext _context;

    public ShiftsRepository(ShiftsDbContext context)
    {
        _context = context;
    }

    public async Task CreateShift(Shift shift)
    {
        await _context.Shifts.AddAsync(shift);
    }
    public async Task UpdateShiftById(Shift shift)
    {

    }

    public async Task<List<Shift>> GetAllShiftsByUserId(int userId)
    {
        return new List<Shift>();
    }

    public async Task DeleteShiftById(int id)
    {

    }

    public async Task SaveChanges()
    {
        await _context.SaveChangesAsync();
    }
}
