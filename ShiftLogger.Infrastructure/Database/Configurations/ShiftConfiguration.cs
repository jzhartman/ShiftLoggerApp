using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShiftLogger.Domain.Models;

namespace ShiftLogger.Infrastructure.Database.Configurations;

internal class ShiftConfiguration : IEntityTypeConfiguration<Shift>
{
    public void Configure(EntityTypeBuilder<Shift> builder)
    {
        builder.ToTable("Shifts");

        builder.HasKey(s => s.Id);

        builder.HasOne(s => s.Employee)
            .WithMany(u => u.Shifts)
            .HasForeignKey(s => s.UserId);

        builder.Property(s => s.ClockInTime)
            .IsRequired();

        builder.Property(s => s.ClockOutTime);

        builder.Property(s => s.IsClockedIn)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(s => s.IsClockedOut)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(s => s.NeedsReviewed)
            .IsRequired()
            .HasDefaultValue(false);
    }
}
