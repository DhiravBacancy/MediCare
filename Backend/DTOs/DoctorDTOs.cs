using MediCare.Models;

namespace MediCare.DTOs
{
    public class CreateDoctorDto
    {
        public int UserId { get; set; }
        public int SpecializationId { get; set; }
        public string Qualification { get; set; }
        public string LicenseNumber { get; set; }
    }

    public class UpdateDoctorDto
    {
        public string Qualification { get; set; }
        public string LicenseNumber { get; set; }
        public int SpecializationId { get; set; }
    }

    public class DoctorDto
    {
        public int DoctorId { get; set; }
        public string Qualification { get; set; }
        public string LicenseNumber { get; set; }
        public string SpecializationName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Constructor to map the model to DTO
        public DoctorDto(Doctor doctor)
        {
            DoctorId = doctor.DoctorId;
            Qualification = doctor.Qualification;
            LicenseNumber = doctor.LicenseNumber;
            SpecializationName = doctor.Specialization.SpecializationName;
            FirstName = doctor.User.FirstName;
            LastName = doctor.User.LastName;
        }
    }
}
