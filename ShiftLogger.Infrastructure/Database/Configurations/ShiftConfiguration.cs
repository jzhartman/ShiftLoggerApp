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
            .WithMany(e => e.Shifts)
            .HasForeignKey(s => s.EmployeeId);

        builder.Property(s => s.ClockInTime)
            .IsRequired();

        builder.Property(s => s.ClockOutTime);
    }
}
