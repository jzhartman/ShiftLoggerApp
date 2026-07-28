using ShiftLogger.Domain.Models;
using ShiftLogger.Infrastructure.Repositories;

namespace ShiftLogger.Application.Employees.Commands.DeleteEmployee;

public class DeleteEmployeeHandler
{
    private readonly IEmployeeRepository _empoyeeRepository;
    private readonly IShiftsRepository _shiftRepository;

    public DeleteEmployeeHandler(IEmployeeRepository empoyeeRepository, IShiftsRepository shiftRepository)
    {
        _empoyeeRepository = empoyeeRepository;
        _shiftRepository = shiftRepository;
    }

    public async Task HandleAsync(DeleteEmployeeCommand command)
    {
        await _shiftRepository.DeleteAllShiftsByEmployeeId(command.Id);

        await _empoyeeRepository.DeleteEmployeeAsync(new Employee
        {
            Id = command.Id,
            FirstName = command.FirstName,
            LastName = command.LastName
        });

        await _empoyeeRepository.SaveChangesAsync();
    }
}
