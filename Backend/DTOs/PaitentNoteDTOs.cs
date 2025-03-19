namespace MediCare.DTOs
{
    public class CreatePatientNoteDto
    {
        public string NoteText { get; set; }
        public string CreatedBy { get; set; }
        public int AppointmentId { get; set; }
    }
    public class UpdatePatientNoteDto
    {
        public string NoteText { get; set; }
        public string UpdatedBy { get; set; }
    }
}
