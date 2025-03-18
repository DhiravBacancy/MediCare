using System;
using System.Collections.Generic;

namespace MediCare.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }

        public int PatientId { get; set; }  // FK to Patient
        public int DoctorId { get; set; }   // FK to Doctor
        public int? ReceptionistId { get; set; }  // FK to Receptionist (nullable)
        public DateTime AppointmentStarts { get; set; }
        public DateTime AppointmentEnds { get; set; }

        public string Status { get; set; }
        public string AppointmentDescription { get; set; }

        // New properties for created and updated information
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties (marked as virtual for lazy loading)
        public virtual Patient Patient { get; set; }
        public virtual Doctor Doctor { get; set; }
        public virtual Receptionist Receptionist { get; set; }

        // One-to-one relationship with PatientNote
        public virtual PatientNote PatientNote { get; set; }

        // One-to-many relationship with Billing
        public virtual ICollection<Billing> Billings { get; set; } = new List<Billing>();
    }
}
