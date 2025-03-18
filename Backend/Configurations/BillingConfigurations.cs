using MediCare.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediCare.Configurations
{
    public class BillingConfiguration : IEntityTypeConfiguration<Billing>
    {
        public void Configure(EntityTypeBuilder<Billing> builder)
        {
            builder.HasKey(b => b.BillingId);  // Set the primary key

            // Configure properties
            builder.Property(b => b.Amount)
                .IsRequired()  // Amount is required
                .HasColumnType("decimal(18,2)");  // Specify the decimal precision for Amount

            builder.Property(b => b.BillingDate)
                .IsRequired();  // BillingDate is required

            builder.Property(b => b.PaymentStatus)
                .IsRequired()  // PaymentStatus is required
                .HasMaxLength(50);  // Max length for PaymentStatus

            // Set up relationships

            builder.HasOne(b => b.Patient)  // One Billing is associated with one Patient
     .WithMany(p => p.Billings)  // A patient can have many Billings
     .HasForeignKey(b => b.PatientId)  // Foreign key for Patient
     .OnDelete(DeleteBehavior.Restrict);  // Prevent deletion of Patient if they are linked to Billing

            builder.HasOne(b => b.Appointment)  // One Billing is associated with one Appointment
                .WithMany(a => a.Billings)  // An appointment can have many Billings
                .HasForeignKey(b => b.AppointmentId)  // Foreign key for Appointment
                .OnDelete(DeleteBehavior.NoAction);  // Prevent cascading delete on Appointment (or use Restrict)

        }
    }
}
