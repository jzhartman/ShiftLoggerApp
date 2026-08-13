using Moq;
using ShiftLogger.Application.Shifts.Commands.CreateShift;
using ShiftLogger.Domain.Models;
using ShiftLogger.Domain.Validation;
using ShiftLogger.Infrastructure.Repositories;

namespace ShiftLogger.Tests.ApplicationTests;

public class CreateShiftHandlerTests
{
    private readonly Mock<IEmployeeRepository> _employeeRepoMock;
    private readonly Mock<IShiftsRepository> _shiftRepoMock;
    private readonly CreateShiftHandler _handler;
    public CreateShiftHandlerTests()
    {
        _employeeRepoMock = new Mock<IEmployeeRepository>();
        _shiftRepoMock = new Mock<IShiftsRepository>();
        _handler = new CreateShiftHandler(_shiftRepoMock.Object, _employeeRepoMock.Object);
    }

    [Fact]
    public async Task HandleAsync_WhenEmployeeDoesNotExist_ShouldReturnFailure()
    {
        // Arrange
        var command = new CreateShiftCommand(
            1,
            DateTime.Parse("2026-08-01 08:00:00"),
            DateTime.Parse("2026-08-01 17:00:00")
        );

        var employeeCheckResult = Result<bool>.Failure();

        _employeeRepoMock
            .Setup(repo => repo.EmployeeExistsById(command.EmployeeId))
            .ReturnsAsync(employeeCheckResult);

        // Act
        var result = await _handler.HandleAsync(command);

        // Assert
        Assert.True(result.IsFailure);
        _shiftRepoMock.Verify(repo => repo.OverlapsExistingShift(It.IsAny<Shift>()), Times.Never);
    }
}
