using MediCare.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediCare.Configurations
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasKey(a => a.AppointmentId);  // Set the primary key

            // Configure the properties
            builder.Property(a => a.AppointmentStarts)
                .IsRequired();  // Appointment start date is required

            builder.Property(a => a.AppointmentEnds)
                .IsRequired();  // Appointment end date is required

            builder.Property(a => a.Status)
                .HasMaxLength(50);  // Set the max length for status

            builder.Property(a => a.AppointmentDescription)
                .HasMaxLength(200);  // Set max length for the appointment description

            // Configure new properties for created and updated information
            builder.Property(a => a.CreatedBy)
                .HasMaxLength(100)  // Max length for CreatedBy
                .IsRequired();  // Ensure CreatedBy is not null

            builder.Property(a => a.CreatedAt)
                .IsRequired();  // CreatedAt is required

            builder.Property(a => a.UpdatedBy)
                .HasMaxLength(100);  // Max length for UpdatedBy

            builder.Property(a => a.UpdatedAt)
                .IsRequired(false);  // UpdatedAt is nullable, since it will only be set if updated

            // Set up the relationships

            builder.HasOne(a => a.Patient)  // One appointment is associated with one patient
                .WithMany(p => p.Appointments)  // A patient can have many appointments
                .HasForeignKey(a => a.PatientId)  // Foreign key for Patient
                .OnDelete(DeleteBehavior.Cascade);  // If a patient is deleted, the related appointments will be deleted

            builder.HasOne(a => a.Doctor)  // One appointment is associated with one doctor
                .WithMany(d => d.Appointments)  // A doctor can have many appointments
                .HasForeignKey(a => a.DoctorId)  // Foreign key for Doctor
                .OnDelete(DeleteBehavior.Cascade);  // If a doctor is deleted, the related appointments will be deleted

            builder.HasOne(a => a.Receptionist)  // One appointment can be linked to one receptionist
                .WithMany()  // A receptionist can handle multiple appointments
                .HasForeignKey(a => a.ReceptionistId)  // Foreign key for Receptionist
                .OnDelete(DeleteBehavior.Restrict);  // Prevent deleting the receptionist if they are associated with an appointment

            builder.HasOne(a => a.PatientNote)  // One appointment can have one patient note
                .WithOne(pn => pn.Appointment)  // One-to-one relationship with PatientNote
                .HasForeignKey<PatientNote>(pn => pn.AppointmentId)  // Set foreign key in PatientNote to link to Appointment
                .OnDelete(DeleteBehavior.Cascade);  // If an appointment is deleted, the associated patient note will be deleted
        }
    }
}
