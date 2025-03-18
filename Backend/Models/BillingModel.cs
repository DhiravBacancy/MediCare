using System;

namespace MediCare.Models
{
    public class Billing
    {
        public int BillingId { get; set; }
        public int PatientId { get; set; }     // FK to Patient
        public int AppointmentId { get; set; } // FK to Appointment

        public decimal Amount { get; set; }
        public DateTime BillingDate { get; set; }
        public string PaymentStatus { get; set; }  // e.g., Paid, Pending

        // Navigation properties (marked as virtual)
        public virtual Patient Patient { get; set; }
        public virtual Appointment Appointment { get; set; }
    }
}
