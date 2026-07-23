using Microsoft.EntityFrameworkCore;
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

    public async Task CreateShiftAsync(Shift shift)
    {
        await _context.Shifts.AddAsync(shift);
    }
    public async Task UpdateShiftByIdAsync(Shift shift)
    {

    }

    public async Task<List<Shift>> GetAllShiftsByUserIdAsync(int employeeId)
    {
        return await _context.Shifts.Where(s => s.EmployeeId == employeeId).ToListAsync();
    }

    public async Task DeleteShiftByIdAsync(int id)
    {

    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
