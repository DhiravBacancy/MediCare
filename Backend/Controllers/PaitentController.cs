using MediCare.DTOs;
using MediCare.Models;
using MediCare_.DTOs;
using MediCare_.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediCare.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IGenericService<Patient> _patientService;

        public PatientController(IGenericService<Patient> patientService)
        {
            _patientService = patientService;
        }

        [HttpPost("filtersearch")]
        public async Task<IActionResult> SearchUsersByFilters([FromBody] List<Filter> filters)
        {
            var note = await _patientService.GetByMultipleConditionsAsync(filters);

            if (!note.Any())
            {
                return NotFound(new { Message = "No Note found matching the criteria" });
            }

            return Ok(note);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatient(int id)
        {
            var patient = await _patientService.GetByIdAsync(id);
            if (patient == null) return NotFound("Patient not found");

            return Ok(new PatientDto(patient));
        }

        // ✅ Create a New Patient
        [HttpPost]
        public async Task<IActionResult> CreatePatient([FromBody] CreatePatientDto model)
        {
            if (string.IsNullOrEmpty(model.FirstName) || string.IsNullOrEmpty(model.LastName))
                return BadRequest("First and Last Name are required");

            var patient = new Patient
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                MobileNo = model.MobileNo,
                Email = model.Email,
                AadharNo = model.AadharNo,
                Address = model.Address,
                City = model.City,
                Active = model.Active,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null
            };

            await _patientService.AddAsync(patient);

            return CreatedAtAction(nameof(GetPatient), new { id = patient.PatientId }, patient);
        }

        // ✅ Update Patient
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient(int id, [FromBody] UpdatePatientDto updatedPatient)
        {
            var patient = await _patientService.GetByIdAsync(id);
            if (patient == null) return NotFound("Patient not found");

            patient.FirstName = updatedPatient.FirstName;
            patient.LastName = updatedPatient.LastName;
            patient.MobileNo = updatedPatient.MobileNo;
            patient.Email = updatedPatient.Email;
            patient.AadharNo = updatedPatient.AadharNo;
            patient.Address = updatedPatient.Address;
            patient.City = updatedPatient.City;
            patient.Active = updatedPatient.Active;
            patient.UpdatedAt = DateTime.UtcNow;

            await _patientService.UpdateAsync(patient);

            return Ok(new PatientDto(patient));
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedPatients([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var pagedResult = await _patientService.GetPaginatedAsync(pageNumber, pageSize);

            if (!pagedResult.Items.Any())
            {
                return NotFound(new { Message = "No patients found" });
            }

            return Ok(pagedResult);
        }

        // ✅ Delete Patient
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var patient = await _patientService.GetByIdAsync(id);
            if (patient == null) return NotFound("Patient not found");

            await _patientService.DeleteAsync(id);

            return Ok(new { message = "Patient deleted successfully" });
        }

        //[HttpGet("{id}/details")]
        //public async Task<IActionResult> GetPatientDetails(int id)
        //{
        //    var patient = await _patientService.GetByIdAsync(id);
        //    if (patient == null) return NotFound("Patient not found");

        //    var appointments = await _appointmentService.GetByPatientIdAsync(id);

        //    var result = new
        //    {
        //        Patient = patient,
        //        Appointments = appointments.Select(a => new
        //        {
        //            a.AppointmentId,
        //            a.AppointmentDate,
        //            a.DoctorName,
        //            Notes = _appointmentNoteService.GetByAppointmentIdAsync(a.AppointmentId).Result
        //        })
        //    };

        //    return Ok(result);
        //}

    }
}
