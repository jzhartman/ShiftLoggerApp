using ShiftLogger.Domain.Models;

namespace ShiftLogger.Infrastructure.Repositories;

public interface IShiftsRepository
{
    Task CreateShift(Shift shift);
    Task DeleteShiftById(int id);
    Task<List<Shift>> GetAllShiftsByUserId(int userId);
    Task UpdateShiftById(Shift shift);
}