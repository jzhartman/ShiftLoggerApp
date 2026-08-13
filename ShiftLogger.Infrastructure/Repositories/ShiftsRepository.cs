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

            if (originalShiftResponse is not null)
            {
                originalShiftResponse.EmployeeId = updatedShift.EmployeeId;
                originalShiftResponse.ClockInTime = updatedShift.ClockInTime;
                originalShiftResponse.ClockOutTime = updatedShift.ClockOutTime;
                originalShiftResponse.Employee = updatedShift.Employee;
            }

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
            _context.Shifts.Remove(shift);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error("DatabaseError", ex.Message));
        }
    }

    public async Task<Result> DeleteAllShiftsByEmployeeId(int employeeId)
    {
        try
        {
            var result = await _context.Shifts.Where(s => s.EmployeeId == employeeId).ExecuteDeleteAsync();

            return (result > 0) ? Result.Success() : Result.Failure(Errors.ShiftsNotFoundForEmployeeId);
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

    public async Task<Result<bool>> ShiftExistsById(int id)
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

    public async Task<Result<bool>> OverlapsExistingShift(Shift shift)
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
    public async Task<Result<bool>> OverlapsExistingShiftsExcludingCurrent(Shift shift)
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
