using MediCare.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediCare.Configurations
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasKey(d => d.DoctorId);  // Define primary key for Doctor

            // Define the properties with configurations
            builder.Property(d => d.Qualification)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(d => d.LicenseNumber)
                .IsRequired()
                .HasMaxLength(50);

            // Configure CreatedAt property
            builder.Property(d => d.CreatedAt)
                .IsRequired();  // Make sure the CreatedAt property is required

            // Define the foreign key relationship with User
            builder.HasOne(d => d.User)  // One Doctor has one User
                .WithOne()  // One User can be assigned to one Doctor
                .HasForeignKey<Doctor>(d => d.UserId)  // UserId is the foreign key for Doctor
                .OnDelete(DeleteBehavior.Restrict);  // Prevent deleting User if it is linked to a Doctor

            // Define the foreign key relationship with Specialization
            builder.HasOne(d => d.Specialization)  // One Doctor has one Specialization
                .WithMany()  // One Specialization can have multiple Doctors
                .HasForeignKey(d => d.SpecializationId)  // SpecializationId is the foreign key for Specialization
                .OnDelete(DeleteBehavior.Restrict);  // Prevent deleting Specialization if linked to a Doctor

            // One-to-many relationship with Appointments
            builder.HasMany(d => d.Appointments)
                .WithOne(a => a.Doctor)  // One Appointment has one Doctor
                .HasForeignKey(a => a.DoctorId)  // DoctorId in Appointment points to Doctor
                .OnDelete(DeleteBehavior.Cascade);  // Cascade delete appointments if Doctor is deleted
        }
    }
}
