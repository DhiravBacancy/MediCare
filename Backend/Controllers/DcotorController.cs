using MediCare.DTOs;
using MediCare.Models;
using MediCare_.Services; // Import the generic service
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
    public class DoctorController : ControllerBase
    {
        private readonly IGenericService<Doctor> _doctorService;

        public DoctorController(IGenericService<Doctor> doctorService)
        {
            _doctorService = doctorService;
        }

        // ✅ Get All Doctors
        [HttpGet]
        public async Task<IActionResult> GetDoctors()
        {
            var doctors = await _doctorService.GetAllAsync();
            var doctorDtos = doctors.Select(d => new DoctorDto(d)).ToList();
            return Ok(doctorDtos);
        }

        // ✅ Get Doctor by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDoctor(int id)
        {
            var doctor = await _doctorService.GetByIdAsync(id);
            if (doctor == null) return NotFound("Doctor not found");
            return Ok(new DoctorDto(doctor));
        }

        // ✅ Create a New Doctor
        [HttpPost]
        public async Task<IActionResult> CreateDoctor([FromBody] CreateDoctorDto model)
        {
            if (model.UserId == 0 || model.SpecializationId == 0)
                return BadRequest("UserId and SpecializationId are required");

            var doctor = new Doctor
            {
                UserId = model.UserId,
                SpecializationId = model.SpecializationId,
                Qualification = model.Qualification,
                LicenseNumber = model.LicenseNumber,
                CreatedAt = DateTime.UtcNow
            };

            await _doctorService.AddAsync(doctor);

            return CreatedAtAction(nameof(GetDoctor), new { id = doctor.DoctorId }, doctor);
        }

        // ✅ Update Doctor
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDoctor(int id, [FromBody] UpdateDoctorDto updatedDoctor)
        {
            var doctor = await _doctorService.GetByIdAsync(id);
            if (doctor == null) return NotFound("Doctor not found");

            doctor.Qualification = updatedDoctor.Qualification;
            doctor.LicenseNumber = updatedDoctor.LicenseNumber;
            doctor.SpecializationId = updatedDoctor.SpecializationId;

            await _doctorService.UpdateAsync(doctor);

            return Ok(new DoctorDto(doctor));
        }

        // ✅ Delete Doctor
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            var doctor = await _doctorService.GetByIdAsync(id);
            if (doctor == null) return NotFound("Doctor not found");

            await _doctorService.DeleteAsync(id);
            return Ok(new { message = "Doctor deleted successfully" });
        }
    }
}
