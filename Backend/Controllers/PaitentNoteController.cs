using MediCare.Data;
using MediCare.DTOs;
using MediCare.Models;
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

        public PatientNoteController(IGenericService<PatientNote> patientNoteService) // Inject the generic service
        {
            _patientNoteService = patientNoteService;
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
                AppointmentId = model.AppointmentId,
                PatientId = model.PatientId
            };

            await _patientNoteService.AddAsync(patientNote); // Use the generic AddAsync

            return Ok(new { Message = "PatientNote created successfully", PatientNote = patientNote });
        }

        // 🔹 Get All Patient Notes for a Patient
        [HttpGet("{patientId}")]
        public async Task<ActionResult<IEnumerable<PatientNote>>> GetPatientNotes(int patientId)
        {
            // This requires filtering, so we need a specific service
            // For now, we get all and filter in memory, but this is inefficient.
            var allNotes = await _patientNoteService.GetAllAsync();
            var patientNotes = allNotes.Where(note => note.PatientId == patientId);

            if (patientNotes == null || !patientNotes.Any())
            {
                return NotFound("No patient notes found.");
            }

            return Ok(patientNotes);
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
    }
}