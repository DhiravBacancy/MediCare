using MediCare.Data;
using MediCare.DTOs;
using MediCare.Models;
using MediCare_.DTOs;
using MediCare_.Services; // Import the service namespace
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediCare.Controllers
{
    //[Authorize(Roles = "Admin, Doctor")]
    [Route("api/[controller]/[action]")]
    public class PatientNoteController : ControllerBase
    {
        private readonly IGenericService<PatientNote> _patientNoteService;
        private readonly IPdfService _pdfService;


        public PatientNoteController(IGenericService<PatientNote> patientNoteService, IPdfService pdfService) // Inject the generic service
        {
            _patientNoteService = patientNoteService;
            _pdfService = pdfService;
        }

        // 🔹 Create Patient Note
        [HttpPost]
        public async Task<ActionResult> CreatePatientNote([FromBody] CreatePatientNoteDto model)
        {
            if (model == null)
                return BadRequest("PatientNote model is null");

            var patientNote = new PatientNote
            {
                NoteText = model.NoteText,
                CreatedBy = model.CreatedBy ?? "System",
                CreatedAt = System.DateTime.UtcNow,
                //AppointmentId = model.AppointmentId,
            };

            await _patientNoteService.AddAsync(patientNote); // Use the generic AddAsync

            return Ok(new { Message = "PatientNote created successfully", PatientNote = patientNote });
        }

        [HttpPost("filtersearch")]
        public async Task<IActionResult> SearchUsersByFilters([FromBody] List<Filter> filters)
        {
            //if (filters == null || !filters.Any())
            //{
            //    return BadRequest(new { Message = "At least one filter must be provided" });
            //}

            var note = await _patientNoteService.GetByMultipleConditionsAsync(filters);

            if (!note.Any())
            {
                return NotFound(new { Message = "No Note found matching the criteria" });
            }

            return Ok(note);
        }

        // 🔹 Get Patient Note by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<PatientNote>> GetPatientNoteById(int id)
        {
            var patientNote = await _patientNoteService.GetByIdAsync(id);

            if (patientNote == null)
                return NotFound(new { Message = "PatientNote not found" });

            return Ok(patientNote);
        }

        // 🔹 Update Patient Note
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePatientNote(int id, [FromBody] UpdatePatientNoteDto model)
        {
            if (model == null)
                return BadRequest("PatientNote model is null");

            var patientNote = await _patientNoteService.GetByIdAsync(id);

            if (patientNote == null)
                return NotFound(new { Message = "PatientNote not found" });

            patientNote.NoteText = model.NoteText;

            await _patientNoteService.UpdateAsync(patientNote);

            return Ok(new { Message = "PatientNote updated successfully", PatientNote = patientNote });
        }

        // 🔹 Delete Patient Note
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatientNote(int id)
        {
            var patientNote = await _patientNoteService.GetByIdAsync(id);
            if (patientNote == null)
                return NotFound(new { Message = "PatientNote not found" });

            await _patientNoteService.DeleteAsync(id);

            return Ok(new { Message = "PatientNote deleted successfully" });
        }

        [HttpGet("DownloadNote")]
        public async Task<IActionResult> DownloadDoctorNotes(int noteId)
        {
            var note = await _patientNoteService.GetByConditionAsync(n => n.PatientNoteId == noteId);
            if (!note.Any())
                return NotFound("No notes found for this doctor.");

            var doctorNote = note.First();  // Get the single note
            var notesText = doctorNote.NoteText;  // Directly access the note text

            var pdfBytes = await _pdfService.GenerateDoctorNotePdfAsync("System", notesText);

            return File(pdfBytes, "application/pdf", $"{noteId}_Notes.pdf");
        }

    }
}