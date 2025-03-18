using MediCare.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediCare.Configurations
{
    public class SpecializationConfiguration : IEntityTypeConfiguration<Specialization>
    {
        public void Configure(EntityTypeBuilder<Specialization> builder)
        {
            builder.HasKey(s => s.SpecializationId);  // Define primary key

            // Define properties
            builder.Property(s => s.SpecializationName)
                .IsRequired()  // SpecializationName is required
                .HasMaxLength(100);  // Max length for SpecializationNamee

            // Set up one-to-many relationship with Doctor
            builder.HasMany(s => s.Doctors)  // One Specialization can have many Doctors
                .WithOne(d => d.Specialization)  // Each Doctor is associated with one Specialization
                .HasForeignKey(d => d.SpecializationId)  // Foreign key on the Doctor entity
                .OnDelete(DeleteBehavior.Cascade);  // Cascade delete for Doctor when Specialization is deleted
        }
    }
}
