namespace MediCare.DTOs
{
    public class CreateSpecializationDto
    {
        public string SpecializationName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
    public class UpdateSpecializationDto
    {
        public string SpecializationName { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UdpatedAt { get; set; }
    }
}
