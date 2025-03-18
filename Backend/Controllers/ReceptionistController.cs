using MediCare.Models;
using MediCare_.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediCare.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReceptionistController : ControllerBase
    {
        private readonly IGenericService<Receptionist> _receptionistService;

        public ReceptionistController(IGenericService<Receptionist> receptionistService)
        {
            _receptionistService = receptionistService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateReceptionist([FromBody] Receptionist model)
        {
            if (model == null)
                return BadRequest("Receptionist model is null");

            model.CreatedAt = DateTime.UtcNow;
            await _receptionistService.AddAsync(model);

            return CreatedAtAction(nameof(GetReceptionist), new { id = model.ReceptionistId }, model);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Receptionist>> GetReceptionist(int id)
        {
            var receptionist = await _receptionistService.GetByIdAsync(id);
            if (receptionist == null)
                return NotFound(new { Message = "Receptionist not found" });

            return Ok(receptionist);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Receptionist>>> GetReceptionists()
        {
            var receptionists = await _receptionistService.GetAllAsync();
            if (receptionists == null || !receptionists.Any())
                return NotFound("No receptionists found.");

            return Ok(receptionists);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReceptionist(int id, [FromBody] Receptionist updatedReceptionist)
        {
            var receptionist = await _receptionistService.GetByIdAsync(id);
            if (receptionist == null) return NotFound("Receptionist not found");

            receptionist.UserId = updatedReceptionist.UserId;
            receptionist.Qualification = updatedReceptionist.Qualification;

            await _receptionistService.UpdateAsync(receptionist);

            return Ok(receptionist);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReceptionist(int id)
        {
            var receptionist = await _receptionistService.GetByIdAsync(id);
            if (receptionist == null) return NotFound("Receptionist not found");

            await _receptionistService.DeleteAsync(id);
            return Ok(new { Message = "Receptionist deleted successfully" });
        }
    }
}
