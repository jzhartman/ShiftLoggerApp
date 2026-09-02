using Microsoft.Extensions.DependencyInjection;
using ShiftLogger.Application.Employees.Commands.CreateEmployee;
using ShiftLogger.Application.Employees.Commands.DeleteEmployee;
using ShiftLogger.Application.Employees.Commands.UpdateEmployee;
using ShiftLogger.Application.Employees.Requests.GetAllEmployees;
using ShiftLogger.Application.Employees.Requests.GetEmployeeById;
using ShiftLogger.Application.Shifts.Commands.CreateShift;
using ShiftLogger.Application.Shifts.Commands.DeleteShift;
using ShiftLogger.Application.Shifts.Commands.UpdateShift;
using ShiftLogger.Application.Shifts.Requests.GetShiftsByEmployeeId;

namespace ShiftLogger.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateShiftHandler>();
        services.AddScoped<GetShiftsByEmployeeIdHandler>();
        services.AddScoped<UpdateShiftHandler>();
        services.AddScoped<DeleteShiftHandler>();

        services.AddScoped<CreateEmployeeHandler>();
        services.AddScoped<GetAllEmpoyeesHandler>();
        services.AddScoped<GetEmployeeByIdHandler>();
        services.AddScoped<UpdateEmployeeHandler>();
        services.AddScoped<DeleteEmployeeHandler>();

        return services;
    }
}
