using System;
using System.Collections.Generic;

namespace MediCare.Models
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        public int UserId { get; set; }          // FK to User
        public int SpecializationId { get; set; }  // FK to Specialization

        public string Qualification { get; set; }
        public string LicenseNumber { get; set; }

        // Navigation properties (marked as virtual)
        public virtual User User { get; set; }
        public virtual Specialization Specialization { get; set; }

        // One-to-many relationship: a doctor can have multiple appointments
        public virtual ICollection<Appointment> Appointments { get; set; }

        // Timestamp for tracking record creation
        public DateTime CreatedAt { get; set; }
    }
}
