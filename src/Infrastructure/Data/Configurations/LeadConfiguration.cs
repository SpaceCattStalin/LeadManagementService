using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class LeadConfiguration : IEntityTypeConfiguration<Lead>
    {
        public void Configure(EntityTypeBuilder<Lead> builder)
        {
            builder.HasKey(l => l.Id);

            builder.Property(l => l.LeadName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(l => l.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(l => l.IntendedMajor)
                .HasMaxLength(100);

            builder.Property(l => l.Status)
                .IsRequired()
                .HasConversion<string>();

            builder.HasMany(l => l.Assignments)
                .WithOne(a => a.Lead)
                .HasForeignKey(a => a.LeadId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(l => l.Interactions)
                .WithOne(i => i.Lead)
                .HasForeignKey(i => i.LeadId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}