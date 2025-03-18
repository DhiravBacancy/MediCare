﻿using System;

namespace MediCare.Models
{
    public class PatientNote
    {
        public int PatientNoteId { get; set; }
        public int AppointmentId { get; set; }  // FK to Appointment
        public int PatientId { get; set; }      // FK to Patient

        public string NoteText { get; set; }

        // Audit fields
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation properties (marked as virtual)
        public virtual Appointment Appointment { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
