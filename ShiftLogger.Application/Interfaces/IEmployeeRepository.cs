using ShiftLogger.Domain.Models;

namespace ShiftLogger.Infrastructure.Repositories;

public interface IEmployeeRepository
{
    Task CreateEmployee(Employee employee);
    Task DeleteEmployee(Employee employee);
    Task<List<Employee>> GetAll();
    Task SaveChangesAsync();
    Task UpdateEmployee(Employee employee);
}