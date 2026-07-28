using ShiftLogger.Application.Employees.Dtos;
using ShiftLogger.Domain.Models;
using ShiftLogger.Infrastructure.Repositories;

namespace ShiftLogger.Application.Employees.Requests.GetAllEmployees;

public class GetAllEmpoyeesHandler
{
    private readonly IEmployeeRepository _employeeRepository;

    public GetAllEmpoyeesHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<List<EmployeeDto>> HandleAsync()
    {
        var employees = await _employeeRepository.GetAllAsync();

        return MapToDto(employees);
    }

    private List<EmployeeDto> MapToDto(List<Employee> employees)
    {
        var output = new List<EmployeeDto>();

        foreach (var employee in employees)
        {
            output.Add(new EmployeeDto(
                employee.Id,
                employee.FirstName,
                employee.LastName));
        }

        return output;
    }
}
