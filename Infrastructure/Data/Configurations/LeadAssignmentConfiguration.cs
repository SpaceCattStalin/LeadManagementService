using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class LeadAssignmentConfiguration : IEntityTypeConfiguration<LeadAssignment>
    {
        public void Configure(EntityTypeBuilder<LeadAssignment> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.AssignedAt)
                .IsRequired();

            builder.Property(a => a.Status)
                .IsRequired()
                .HasConversion<string>();

            // Many-to-one with Lead
            builder.HasOne(a => a.Lead)
                .WithMany(l => l.Assignments)
                .HasForeignKey(a => a.LeadId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}