using ShiftLogger.Console.ApiClients.Employees;

namespace ShiftLogger.Console.Presentation.Services;

internal class SelectEmployeeService
{
    private readonly IEmployeeApiClient _employeeApiClient;

    public SelectEmployeeService(IEmployeeApiClient employeeApiClient)
    {
        _employeeApiClient = employeeApiClient;
    }

    public async Task RunAsync()
    {
        var result = await _employeeApiClient.GetAllAsync();

        foreach (var employee in result.Value)
        {
            System.Console.WriteLine($"{employee.FirstName} {employee.LastName}");
        }


        System.Console.ReadLine();
    }

}
