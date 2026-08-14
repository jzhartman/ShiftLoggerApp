using Moq;
using ShiftLogger.Application.Shifts.Commands.CreateShift;
using ShiftLogger.Domain.Models;
using ShiftLogger.Domain.Validation;
using ShiftLogger.Domain.Validation.Errors;
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

    [Fact]
    public async Task HandleAsync_WhenClockInIsAfterClockOut_ShouldReturnFailure()
    {
        // Arrange
        var command = new CreateShiftCommand(
            1,
            DateTime.Parse("2026-08-01 17:00:00"),
            DateTime.Parse("2026-08-01 08:00:00")
        );

        _employeeRepoMock
            .Setup(repo => repo.EmployeeExistsById(command.EmployeeId))
            .ReturnsAsync(Result<bool>.Success(true));

        // Act
        var result = await _handler.HandleAsync(command);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(Errors.ClockInTimePrecedesClockOutTime, result.Errors[0]);
        _shiftRepoMock.Verify(repo => repo.OverlapsExistingShift(It.IsAny<Shift>()), Times.Never);

    }

    [Fact]
    public async Task HandleAsync_WhenShiftOverlaps_ReturnsFailure()
    {
        // Arrange
        var command = new CreateShiftCommand(
            1,
            DateTime.Parse("2026-08-01 08:00:00"),
            DateTime.Parse("2026-08-01 17:00:00")
        );

        _employeeRepoMock
            .Setup(repo => repo.EmployeeExistsById(command.EmployeeId))
            .ReturnsAsync(Result<bool>.Success(true));

        // Simulate an overlap match
        _shiftRepoMock
            .Setup(repo => repo.OverlapsExistingShift(It.IsAny<Shift>()))
            .ReturnsAsync(Result<bool>.Success(true));

        // Act
        var result = await _handler.HandleAsync(command);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(Errors.NewShiftOverlapsExistingShift, result.Errors[0]);
        _shiftRepoMock.Verify(repo => repo.CreateShiftAsync(It.IsAny<Shift>()), Times.Never);
    }

    [Fact]
    public async Task HandleAsync_WhenEverythingIsValid_CreatesAndSavesShift()
    {
        // Arrange
        var command = new CreateShiftCommand(
            1,
            DateTime.Parse("2026-08-01 08:00:00"),
            DateTime.Parse("2026-08-01 17:00:00")
        );

        _employeeRepoMock
            .Setup(repo => repo.EmployeeExistsById(command.EmployeeId))
            .ReturnsAsync(Result<bool>.Success(true));

        _shiftRepoMock
            .Setup(repo => repo.OverlapsExistingShift(It.IsAny<Shift>()))
            .ReturnsAsync(Result<bool>.Success(false));

        _shiftRepoMock
            .Setup(repo => repo.CreateShiftAsync(It.IsAny<Shift>()))
            .ReturnsAsync(Result.Success());

        _shiftRepoMock
            .Setup(repo => repo.SaveChangesAsync())
            .ReturnsAsync(Result.Success());

        // Act
        var result = await _handler.HandleAsync(command);

        // Assert
        Assert.True(result.IsSuccess);

        // Verify that the shift was actually created and saved to the DB
        _shiftRepoMock.Verify(repo => repo.CreateShiftAsync(It.Is<Shift>(s =>
            s.EmployeeId == command.EmployeeId &&
            s.ClockInTime == command.ClockInTime &&
            s.ClockOutTime == command.ClockOutTime
        )), Times.Once);

        _shiftRepoMock.Verify(repo => repo.SaveChangesAsync(), Times.Once);
    }
}