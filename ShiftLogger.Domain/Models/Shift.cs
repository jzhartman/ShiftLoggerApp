namespace ShiftLogger.Domain.Models;

public class Shift
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime ClockInTime { get; set; }
    public DateTime ClockOutTime { get; set; }
    public bool IsClockedIn { get; set; } = true;
    public bool IsClockedOut { get; set; } = false;
    public bool NeedsReviewed { get; set; } = false;
    public User Employee { get; set; }
}
