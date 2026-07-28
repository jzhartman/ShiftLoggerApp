using ShiftLogger.Domain.Models;

namespace ShiftLogger.Infrastructure.Repositories;

public interface IEmployeeRepository
{
    Task CreateEmployeeAsync(Employee employee);
    Task DeleteEmployeeAsync(Employee employee);
    Task<List<Employee>> GetAllAsync();
    Task SaveChangesAsync();
    Task UpdateEmployeeAsync(Employee employee);
}