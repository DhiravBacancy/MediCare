using MediCare.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediCare.Configurations
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasKey(p => p.PatientId); // Define the primary key

            // Define properties
            builder.Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Email)
                .HasMaxLength(100);

            builder.Property(p => p.MobileNo)
                .HasMaxLength(15);

            builder.Property(p => p.AadharNo)
                .HasMaxLength(20);

            builder.Property(p => p.Address)
                .HasMaxLength(255); // Assuming address will not exceed 255 characters

            builder.Property(p => p.City)
                .HasMaxLength(50); // Assuming city name will not exceed 50 characters

            builder.Property(p => p.Active)
                .IsRequired();

            // New properties for CreatedBy, CreatedAt, UpdatedBy, UpdatedAt
            builder.Property(p => p.CreatedBy)
                .HasMaxLength(50);  // Adjust max length based on your needs

            builder.Property(p => p.CreatedAt)
                .IsRequired();  // Ensure CreatedAt is required

            builder.Property(p => p.UpdatedBy)
                .HasMaxLength(50);  // Adjust max length based on your needs

            builder.Property(p => p.UpdatedAt)
                .IsRequired(false);  // This is nullable since the record may not have been updated yet

            // Set up relationships
            builder.HasMany(p => p.Appointments) // A patient can have many appointments
                .WithOne(a => a.Patient) // An appointment is associated with one patient
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Cascade);
       

            //builder.HasMany(p => p.Billings) // A patient can have many billings
            //    .WithOne(b => b.Patient) // A billing is for one patient
            //    .HasForeignKey(b => b.PatientId)
            //    .OnDelete(DeleteBehavior.Cascade);

            //builder.HasMany(p => p.PatientNotes) // A patient can have many patient notes
            //    .WithOne(pn => pn.Patient) // Each note is linked to one patient
            //    .HasForeignKey(pn => pn.PatientId) // Foreign key to Patient
            //    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
