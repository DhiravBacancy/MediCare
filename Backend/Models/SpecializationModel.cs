using System;
using System.Collections.Generic;

namespace MediCare.Models
{
    public class Specialization
    {
        public int SpecializationId { get; set; }
        public string SpecializationName { get; set; }

        // Navigation property for one-to-many relationship with Doctor (marked as virtual)
        public virtual ICollection<Doctor> Doctors { get; set; }
    }
}
