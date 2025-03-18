using MediCare.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediCare.Configurations
{
    public class ReceptionistConfiguration : IEntityTypeConfiguration<Receptionist>
    {
        public void Configure(EntityTypeBuilder<Receptionist> builder)
        {
            builder.HasKey(r => r.ReceptionistId);  // Define primary key

            // Define properties
            builder.Property(r => r.Qualification)
                .HasMaxLength(100);  // Max length for Qualification

            builder.Property(r => r.CreatedAt)
                .IsRequired();  // CreatedAt is required

            // Set up foreign key relationship with User
            builder.HasOne(r => r.User)  // One Receptionist is associated with one User
                .WithOne()  // A User can only be associated with one Receptionist
                .HasForeignKey<Receptionist>(r => r.UserId)  // Foreign key to User
                .OnDelete(DeleteBehavior.Restrict);  // Prevent deletion of User if linked to a Receptionist

            // Set up one-to-many relationship with Appointment
            builder.HasMany(r => r.Appointments)  // A Receptionist can have many Appointments
                .WithOne(a => a.Receptionist)  // Each Appointment is linked to one Receptionist
                .HasForeignKey(a => a.ReceptionistId)  // Foreign key in Appointment
                .OnDelete(DeleteBehavior.SetNull);  // If a Receptionist is deleted, set the ReceptionistId to null in the associated appointments
        }
    }
}
