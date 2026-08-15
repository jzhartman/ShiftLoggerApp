using ShiftLogger.Domain.Models;
using ShiftLogger.Domain.Validation;

namespace ShiftLogger.Infrastructure.Repositories;

public interface IShiftsRepository
{
    Task<Result> CreateShiftAsync(Shift shift);
    Task<Result> DeleteShiftAsync(Shift shift);
    Task<Result<List<Shift>>> GetAllShiftsByUserIdAsync(int userId);
    Task<Result> UpdateShiftByIdAsync(Shift shift);
    Task<Result> SaveChangesAsync();
    Task<Result> DeleteAllShiftsByEmployeeIdAsync(int employeeId);
    Task<Result<bool>> ShiftExistsByIdAsync(int id);
    Task<Result<bool>> OverlapsExistingShiftAsync(Shift shift);
    Task<Result<bool>> OverlapsExistingShiftsExcludingCurrentAsync(Shift shift);
    Task<Result<int>> ShiftCountByEmployeeIdAsync(int employeeId);
}