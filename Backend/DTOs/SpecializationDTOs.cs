namespace MediCare.DTOs
{
    public class CreateSpecializationDto
    {
        public string SpecializationName { get; set; }
        public string CreatedBy { get; set; }
    }
    public class UpdateSpecializationDto
    {
        public string SpecializationName { get; set; }
        public string UpdatedBy { get; set; }
    }
}
