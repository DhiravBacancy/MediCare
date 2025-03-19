using System;
using System.Collections.Generic;

namespace MediCare.Models
{
    public class Specialization
    {
        public int SpecializationId { get; set; }
        public string SpecializationName { get; set; }

        // Audit fields
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation property for one-to-many relationship with Doctor (marked as virtual)
        public virtual ICollection<Doctor> Doctors { get; set; }
    }
}
