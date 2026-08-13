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
    Task<Result> DeleteAllShiftsByEmployeeId(int employeeId);
    Task<Result<bool>> ShiftExistsById(int id);
    Task<Result<bool>> OverlapsExistingShift(Shift shift);
    Task<Result<bool>> OverlapsExistingShiftsExcludingCurrent(Shift shift);
}