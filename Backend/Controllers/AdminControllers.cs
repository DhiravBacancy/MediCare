using MediCare.DTOs;
using MediCare.Models;
using MediCare.Services;
using MediCare_.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediCare.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]/[action]")]
    public class AdminController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly IGenericService<User> _userService;
        private readonly IGenericService<Role> _roleService;// Use Generic Service

        public AdminController(IEmailService emailService, IGenericService<User> userService, IGenericService<Role> roleSerivce)
        {
            _emailService = emailService;
            _userService = userService;
            _roleService = roleSerivce;
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser([FromBody] UserDTOs model)
        {
            if (model == null)
                return BadRequest("User model is null");

            //var users = await _userService.GetAllAsync();
            var role = await _roleService.GetByIdAsync(model.RoleId);
            if (role == null)
                return BadRequest("Invalid role. Role must be 'Admin', 'Doc', or 'Receptionist'.");

            //if (role.RoleName == "Admin" && users.Any(u => u.Role.RoleName == "Admin"))
            //    return BadRequest("Admin already exists. Only one admin is allowed.");

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);

            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = hashedPassword,
                MobileNo = model.MobileNo,
                EmergencyNo = model.EmergencyNo,
                RoleId = role.RoleId,
                Active = model.Active,
                CreatedBy = model.CreatedBy ?? "",
                CreatedAt = DateTime.UtcNow,
                UpdatedBy = null,
                UpdatedAt = DateTime.UtcNow
            };

            await _userService.AddAsync(user);

            var resetLink = $"https://yourfrontend.com/reset-password?email={user.Email}&token=someGeneratedToken";
            await _emailService.SendResetPasswordEmailAsync(user.Email, resetLink);

            return Ok(new { Message = $"{role.RoleName} user created successfully and reset password link sent." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound(new { Message = "User not found" });

            await _userService.DeleteAsync(id);

            return Ok(new { Message = "User deleted successfully" });
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _userService.GetAllAsync();
            if (users == null || !users.Any())
                return NotFound("No users found.");

            return Ok(users);
        }

        [HttpGet]
        public async Task<ActionResult<User>> GetUser([FromQuery] int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
                return NotFound(new { Message = "User not found" });

            return Ok(user);
        }
    }
}
