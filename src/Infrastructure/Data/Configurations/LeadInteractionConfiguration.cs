using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class LeadInteractionConfiguration : IEntityTypeConfiguration<LeadInteraction>
    {
        public void Configure(EntityTypeBuilder<LeadInteraction> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.InteractionDate)
                .IsRequired();

            builder.Property(i => i.InteractionBy)
                .IsRequired()
                .HasConversion<string>();

            // Many-to-one with Lead
            builder.HasOne(i => i.Lead)
                .WithMany(l => l.Interactions)
                .HasForeignKey(i => i.LeadId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}