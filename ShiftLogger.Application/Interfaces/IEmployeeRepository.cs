using ShiftLogger.Domain.Models;
using ShiftLogger.Domain.Validation;

namespace ShiftLogger.Infrastructure.Repositories;

public interface IEmployeeRepository
{
    Task<Result> CreateEmployeeAsync(Employee employee);
    Task<Result> DeleteEmployeeAsync(Employee employee);
    Task<Result<bool>> EmployeeExistsByFullNameAsync(Employee employee);
    Task<Result<bool>> EmployeeExistsByIdAsync(int id);
    Task<Result<List<Employee>>> GetAllAsync();
    Task<Result<Employee?>> GetEmployeeByIdAsync(int id);
    Task<Result> SaveChangesAsync();
    Task<Result> UpdateEmployeeAsync(Employee employee);
}