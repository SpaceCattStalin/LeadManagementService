using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Infrastructure.Persistence.Configurations
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.UserFullName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(b => b.UserEmail)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(b => b.UserPhoneNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(b => b.InterestedCampus)
                .HasMaxLength(100);

            builder.Property(b => b.InterestedAcademicField)
                .HasMaxLength(100);

            builder.Property(b => b.InterestedSpecialization)
                .HasMaxLength(100);

            builder.Property(b => b.InterestedCourse)
                .HasMaxLength(100);

            builder.Property(b => b.Reason)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(b => b.Status)
                .IsRequired();

            builder.Property(b => b.CreatedByUserId)
                .IsRequired()
                .HasMaxLength(36);

            builder.Property(b => b.ClaimedByConsultantId)
                .HasMaxLength(36);

            builder.HasMany(b => b.BookingHistories)
                .WithOne(h => h.Booking)
                .HasForeignKey(h => h.BookingId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
