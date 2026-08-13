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
    public async Task CreateEmployeeAsync(Employee employee)
    {
        await _context.Employees.AddAsync(employee);
    }
    public async Task<List<Employee>> GetAllAsync()
    {
        var employees = await _context.Employees.ToListAsync();

        return employees;
    }
    // ToDo: Change update method to not use this style
    public async Task UpdateEmployeeAsync(Employee updatedEmployee)
    {
        var originalEmployee = await _context.Employees.FindAsync(updatedEmployee.Id);

        if (originalEmployee is not null)
        {
            originalEmployee.FirstName = updatedEmployee.FirstName;
            originalEmployee.LastName = updatedEmployee.LastName;
        }
    }
    public async Task DeleteEmployeeAsync(Employee employee)
    {
        _context.Employees.Remove(employee);
    }
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
    public async Task<Result<bool>> EmployeeExistsById(int id)
    {
        try
        {
            var response = await _context.Employees.FindAsync(id);

            return (response is null) ? Result<bool>.Success(true) : Result<bool>.Failure(Errors.EmployeeNotFound);
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure(new Error("DatabaseError", ex.Message));
        }
    }
}
