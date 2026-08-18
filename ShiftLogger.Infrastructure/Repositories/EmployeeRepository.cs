using Microsoft.EntityFrameworkCore;
using ShiftLogger.Domain.Models;
using ShiftLogger.Domain.Validation;
using ShiftLogger.Domain.Validation.Errors;
using ShiftLogger.Infrastructure.Database;

namespace ShiftLogger.Infrastructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly ShiftsDbContext _context;

    public EmployeeRepository(ShiftsDbContext context)
    {
        _context = context;
    }
    public async Task<Result> CreateEmployeeAsync(Employee employee)
    {
        try
        {
            var result = await _context.Employees.AddAsync(employee);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error("DatabaseError", ex.Message));
        }
    }
    public async Task<Result<List<Employee>>> GetAllAsync()
    {
        try
        {
            var response = await _context.Employees.ToListAsync();

            return Result<List<Employee>>.Success(response);
        }
        catch (Exception ex)
        {
            return Result<List<Employee>>.Failure(new Error("DatabaseError", ex.Message));
        }
    }
    public async Task<Result> UpdateEmployeeAsync(Employee updatedEmployee)
    {
        try
        {
            var originalEmployeeResponse = await _context.Employees.FindAsync(updatedEmployee.Id);

            if (originalEmployeeResponse is null)
                return Result.Failure(Errors.EmployeeNotFound);

            originalEmployeeResponse.FirstName = updatedEmployee.FirstName;
            originalEmployeeResponse.LastName = updatedEmployee.LastName;

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error("DatabaseError", ex.Message));
        }
    }
    public async Task<Result> DeleteEmployeeAsync(Employee employee)
    {
        try
        {
            await _context.Employees
                .Where(e => e.Id == employee.Id)
                .ExecuteDeleteAsync();

            return Result.Success();
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
    public async Task<Result<bool>> EmployeeExistsByIdAsync(int id)
    {
        try
        {
            var response = await _context.Employees.AnyAsync(e => e.Id == id);

            return Result<bool>.Success(response);
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure(new Error("DatabaseError", ex.Message));
        }
    }

    public async Task<Result<bool>> EmployeeExistsByFullNameAsync(Employee employee)
    {
        try
        {
            var response = await _context.Employees.AnyAsync(e => e.FirstName == employee.FirstName &&
                                                                    e.LastName == employee.LastName);

            return Result<bool>.Success(response);
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure(new Error("DatabaseError", ex.Message));
        }
    }
}
