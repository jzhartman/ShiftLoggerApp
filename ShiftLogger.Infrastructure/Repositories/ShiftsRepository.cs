using Microsoft.EntityFrameworkCore;
using ShiftLogger.Domain.Models;
using ShiftLogger.Domain.Validation;
using ShiftLogger.Domain.Validation.Errors;
using ShiftLogger.Infrastructure.Database;

namespace ShiftLogger.Infrastructure.Repositories;

public class ShiftsRepository : IShiftsRepository
{
    private readonly ShiftsDbContext _context;

    public ShiftsRepository(ShiftsDbContext context)
    {
        _context = context;
    }

    public async Task<Result> CreateShiftAsync(Shift shift)
    {
        try
        {
            await _context.Shifts.AddAsync(shift);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error("DatabaseError", ex.Message));
        }
    }

    public async Task<Result<List<Shift>>> GetAllShiftsByUserIdAsync(int employeeId)
    {
        try
        {
            var response = await _context.Shifts.Where(s => s.EmployeeId == employeeId).ToListAsync();

            if (response is null || response.Count == 0)
                response = new List<Shift>();

            return Result<List<Shift>>.Success(response);
        }
        catch (Exception ex)
        {
            return Result<List<Shift>>.Failure(new Error("DatabaseError", ex.Message));
        }
    }

    // ToDo: Change update method to not use this style
    public async Task<Result> UpdateShiftByIdAsync(Shift updatedShift)
    {
        try
        {
            var originalShiftResponse = await _context.Shifts.FindAsync(updatedShift.Id);

            if (originalShiftResponse is null)
                return Result.Failure(Errors.ShiftIdNotFound);


            originalShiftResponse.EmployeeId = updatedShift.EmployeeId;
            originalShiftResponse.ClockInTime = updatedShift.ClockInTime;
            originalShiftResponse.ClockOutTime = updatedShift.ClockOutTime;

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error("DatabaseError", ex.Message));
        }
    }

    public async Task<Result> DeleteShiftAsync(Shift shift)
    {
        try
        {
            await _context.Shifts
                .Where(s => s.Id == shift.Id)
                .ExecuteDeleteAsync();

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error("DatabaseError", ex.Message));
        }
    }

    public async Task<Result<int>> ShiftCountByEmployeeIdAsync(int employeeId)
    {
        try
        {
            var response = await _context.Shifts.CountAsync(s => s.EmployeeId == employeeId);

            return Result<int>.Success(response) ?? Result<int>.Failure(Errors.ShiftCountNull);
        }
        catch (Exception ex)
        {
            return Result<int>.Failure(new Error("DatabaseError", ex.Message));
        }
    }

    public async Task<Result> DeleteAllShiftsByEmployeeIdAsync(int employeeId)
    {
        try
        {
            var response = await _context.Shifts.Where(s => s.EmployeeId == employeeId).ExecuteDeleteAsync();

            return (response > 0) ? Result.Success() : Result.Failure(Errors.ShiftsNotFoundForEmployeeId);
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error("DatabaseError", ex.Message));
        }
    }

    public async Task<Result> SaveChangesAsync()
    {
        try
        {
            var result = await _context.SaveChangesAsync();

            return (result > 0) ? Result.Success() : Result.Failure(Errors.NoSaveData);
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error("DatabaseError", ex.Message));
        }
    }

    public async Task<Result<bool>> ShiftExistsByIdAsync(int id)
    {
        try
        {
            var response = await _context.Shifts.FindAsync(id);

            bool shiftExists = (response is null) ? false : true;

            return Result<bool>.Success(shiftExists);
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure(new Error("DatabaseError", ex.Message));
        }
    }

    public async Task<Result<bool>> OverlapsExistingShiftAsync(Shift shift)
    {
        try
        {
            var response = await _context.Shifts
                .AnyAsync(s =>
                    s.EmployeeId == shift.EmployeeId &&
                    shift.ClockInTime < s.ClockOutTime &&
                    shift.ClockOutTime > s.ClockInTime);

            return Result<bool>.Success(response) ?? Result<bool>.Failure(Errors.ShiftOverlapReturnedNull);
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure(new Error("DatabaseError", ex.Message));
        }
    }
    public async Task<Result<bool>> OverlapsExistingShiftsExcludingCurrentAsync(Shift shift)
    {
        try
        {
            var response = await _context.Shifts
                .AnyAsync(s =>
                    s.EmployeeId == shift.EmployeeId &&
                    s.Id != shift.Id &&
                    shift.ClockInTime < s.ClockOutTime &&
                    shift.ClockOutTime > s.ClockInTime);

            return Result<bool>.Success(response) ?? Result<bool>.Failure(Errors.ShiftOverlapReturnedNull);
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure(new Error("DatabaseError", ex.Message));
        }
    }
}
