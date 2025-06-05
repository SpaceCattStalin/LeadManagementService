using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Infrastructure.Persistence.Configurations
{
    public class BookingHistoryConfiguration : IEntityTypeConfiguration<BookingHistory>
    {
        public void Configure(EntityTypeBuilder<BookingHistory> builder)
        {
            builder.HasKey(bh => bh.Id);

            builder.Property(bh => bh.Content)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(bh => bh.BookingId)
                .IsRequired();

            builder.HasOne(bh => bh.Booking)
                .WithMany(b => b.BookingHistories)
                .HasForeignKey(bh => bh.BookingId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
