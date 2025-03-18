using System;
using System.Collections.Generic;

namespace MediCare.Models
{
    public class Patient
    {
        public int PatientId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string AadharNo { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }

        // Audit fields
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties (marked as virtual)
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Billing> Billings { get; set; }
        public virtual ICollection<PatientNote> PatientNotes { get; set; }
    }
}
