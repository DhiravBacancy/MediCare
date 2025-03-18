using MediCare.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediCare.Configurations
{
    public class PatientNoteConfiguration : IEntityTypeConfiguration<PatientNote>
    {
        public void Configure(EntityTypeBuilder<PatientNote> builder)
        {
            builder.HasKey(pn => pn.PatientNoteId);  // Define primary key

            // Define properties
            builder.Property(pn => pn.NoteText)
                .IsRequired()  // NoteText is required
                .HasMaxLength(500);  // Max length for NoteText

            // New properties for CreatedBy and CreatedAt
            builder.Property(pn => pn.CreatedBy)
                .HasMaxLength(50);  // Adjust max length as needed

            builder.Property(pn => pn.CreatedAt)
                .IsRequired();  // Ensure that CreatedAt is required

            // Set up relationships
            builder.HasOne(pn => pn.Appointment)
                .WithOne(a => a.PatientNote)
                .HasForeignKey<PatientNote>(pn => pn.AppointmentId)
                .OnDelete(DeleteBehavior.Restrict);  // Or DeleteBehavior.SetNull

            builder.HasOne(pn => pn.Patient)  // One PatientNote belongs to one Patient
                .WithMany(p => p.PatientNotes)  // One Patient can have many PatientNotes
                .HasForeignKey(pn => pn.PatientId)
                .OnDelete(DeleteBehavior.Cascade);  // Cascade delete for Patient
        }
    }
}
