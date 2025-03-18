using MediCare.DTOs;
using MediCare.Models;
using MediCare_.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        // ✅ Get All Patients (with search)
        [HttpGet]
        public async Task<IActionResult> GetPatients(string? search, int page = 1, int pageSize = 10)
        {
            var patients = await _patientService.GetAllAsync();

            if (!string.IsNullOrEmpty(search))
            {
                patients = patients.Where(p =>
                    p.FirstName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    p.LastName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    p.MobileNo.Contains(search)).ToList();
            }

            var totalRecords = patients.Count();
            var paginatedPatients = patients.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var patientDtos = paginatedPatients.Select(p => new PatientDto(p)).ToList();

            return Ok(new { TotalRecords = totalRecords, Data = patientDtos });
        }

        // ✅ Get Patient by ID
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

        // ✅ Delete Patient
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var patient = await _patientService.GetByIdAsync(id);
            if (patient == null) return NotFound("Patient not found");

            await _patientService.DeleteAsync(id);

            return Ok(new { message = "Patient deleted successfully" });
        }
    }
}
