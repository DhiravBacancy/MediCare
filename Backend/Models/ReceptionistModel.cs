using System;
using System.Collections.Generic;

namespace MediCare.Models
{
    public class Receptionist
    {
        public int ReceptionistId { get; set; }  // Primary key, non-nullable
        public int UserId { get; set; }          // FK to User

        public string Qualification { get; set; }
        public DateTime CreatedAt { get; set; }  // When the Receptionist was created

        // Navigation properties (marked as virtual)
        public virtual User User { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
