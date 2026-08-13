using ShiftLogger.Domain.Models;
using ShiftLogger.Domain.Validation;

namespace ShiftLogger.Infrastructure.Repositories;

public interface IEmployeeRepository
{
    Task CreateEmployeeAsync(Employee employee);
    Task DeleteEmployeeAsync(Employee employee);
    Task<Result<bool>> EmployeeExistsById(int id);
    Task<List<Employee>> GetAllAsync();
    Task SaveChangesAsync();
    Task UpdateEmployeeAsync(Employee employee);
}