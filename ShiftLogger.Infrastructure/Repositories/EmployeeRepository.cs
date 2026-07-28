using Microsoft.EntityFrameworkCore;
using ShiftLogger.Domain.Models;
using ShiftLogger.Infrastructure.Database;

namespace ShiftLogger.Infrastructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly ShiftsDbContext _context;

    public EmployeeRepository(ShiftsDbContext context)
    {
        _context = context;
    }
    public async Task CreateEmployee(Employee employee)
    {
        await _context.Employees.AddAsync(employee);
    }
    public async Task<List<Employee>> GetAll()
    {
        var employees = await _context.Employees.ToListAsync();

        return employees;
    }
    public async Task UpdateEmployee(Employee employee)
    {
        var originalEmployee = await _context.Employees.FindAsync(employee.Id);

        if (originalEmployee is not null)
        {
            originalEmployee.FirstName = employee.FirstName;
            originalEmployee.LastName = employee.LastName;
        }
    }
    public async Task DeleteEmployee(Employee employee)
    {
        _context.Employees.Remove(employee);
    }
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
