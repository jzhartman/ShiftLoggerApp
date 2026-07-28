using ShiftLogger.Domain.Models;

namespace ShiftLogger.Infrastructure.Repositories;

public interface IShiftsRepository
{
    Task CreateShiftAsync(Shift shift);
    Task DeleteShiftAsync(Shift shift);
    Task<List<Shift>> GetAllShiftsByUserIdAsync(int userId);
    Task UpdateShiftByIdAsync(Shift shift);
    Task SaveChangesAsync();
    Task DeleteAllShiftsByEmployeeId(int employeeId);
}