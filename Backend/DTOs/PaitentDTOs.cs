using MediCare.Models;

namespace MediCare.DTOs
{
    public class CreatePatientDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string AadharNo { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public bool Active { get; set; }
    }

    public class UpdatePatientDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string AadharNo { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public bool Active { get; set; }
    }

    public class PatientDto
    {
        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string AadharNo { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Constructor to map the model to DTO
        public PatientDto(Patient patient)
        {
            PatientId = patient.PatientId;
            FirstName = patient.FirstName;
            LastName = patient.LastName;
            MobileNo = patient.MobileNo;
            Email = patient.Email;
            AadharNo = patient.AadharNo;
            Address = patient.Address;
            City = patient.City;
            Active = patient.Active;
            CreatedAt = patient.CreatedAt;
            UpdatedAt = patient.UpdatedAt;
        }
    }
}
