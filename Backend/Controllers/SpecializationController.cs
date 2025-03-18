using MediCare.Data;
using MediCare.DTOs;
using MediCare.Models;
using MediCare_.Services; // Import the generic service
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediCare.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]/[action]")]
    public class SpecializationController : ControllerBase
    {
        private readonly IGenericService<Specialization> _specializationService; // Inject the generic service

        public SpecializationController(IGenericService<Specialization> specializationService)
        {
            _specializationService = specializationService;
        }

        // 🔹 Create Specialization
        [HttpPost]
        public async Task<ActionResult> CreateSpecialization([FromBody] CreateSpecializationDto model)
        {
            if (model == null)
                return BadRequest("Specialization model is null");

            var specialization = new Specialization
            {
                SpecializationName = model.SpecializationName,
            };

            await _specializationService.AddAsync(specialization); // Use the generic AddAsync

            return Ok(new { Message = "Specialization created successfully", Specialization = specialization });
        }

        // 🔹 Get All Specializations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Specialization>>> GetSpecializations()
        {
            try
            {
                var specializations = await _specializationService.GetAllAsync(); // Use the generic GetAllAsync

                if (specializations == null || !specializations.Any())
                {
                    return NotFound("No specializations found.");
                }

                return Ok(specializations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // 🔹 Get Specialization by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Specialization>> GetSpecialization(int id)
        {
            var specialization = await _specializationService.GetByIdAsync(id); // Use the generic GetByIdAsync

            if (specialization == null)
                return NotFound(new { Message = "Specialization not found" });

            return Ok(specialization);
        }

        // 🔹 Update Specialization
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateSpecialization(int id, [FromBody] UpdateSpecializationDto model)
        {
            if (model == null)
                return BadRequest("Specialization model is null");

            var specialization = await _specializationService.GetByIdAsync(id); // Use the generic GetByIdAsync

            if (specialization == null)
                return NotFound(new { Message = "Specialization not found" });

            specialization.SpecializationName = model.SpecializationName;

            await _specializationService.UpdateAsync(specialization); // Use the generic UpdateAsync

            return Ok(new { Message = "Specialization updated successfully", Specialization = specialization });
        }

        // 🔹 Delete Specialization
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpecialization(int id)
        {
            var specialization = await _specializationService.GetByIdAsync(id); // Use the generic GetByIdAsync

            if (specialization == null)
                return NotFound(new { Message = "Specialization not found" });

            await _specializationService.DeleteAsync(id); // Use the generic DeleteAsync

            return Ok(new { Message = "Specialization deleted successfully" });
        }
    }
}