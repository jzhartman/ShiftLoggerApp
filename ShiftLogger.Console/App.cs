using ShiftLogger.Console.ApiClients.Employees;

namespace ShiftLogger.Console;

internal class App
{
    private readonly IEmployeeApiClient _employeeApiClient;

    public App(IEmployeeApiClient employeeApiClient)
    {
        _employeeApiClient = employeeApiClient;
    }

    public async Task RunAsync()
    {
        System.Console.WriteLine("Running app...");

        var result = await _employeeApiClient.GetAllAsync();

        foreach (var employee in result.Value)
        {
            System.Console.WriteLine($"{employee.FirstName} {employee.LastName}");
        }


        System.Console.ReadLine();
    }
}
