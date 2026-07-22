using ShiftLogger.Domain.Models;

namespace ShiftLogger.Infrastructure.Repositories;

public interface IShiftsRepository
{
    Task CreateShiftAsync(Shift shift);
    Task DeleteShiftByIdAsync(int id);
    Task<List<Shift>> GetAllShiftsByUserIdAsync(int userId);
    Task UpdateShiftByIdAsync(Shift shift);
    Task SaveChangesAsync();
}