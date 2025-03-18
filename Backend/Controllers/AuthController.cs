using MediCare.DTOs;
using MediCare.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MediCare.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // 🔹 Login - Returns Access Token & Refresh Token
        [HttpPost]
        public async Task<ActionResult> Login([FromBody] AuthDTOs model)
        {
            if (model == null)
                return BadRequest("Invalid login attempt");

            // Validate the model before proceeding
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var tokens = await _authService.LoginAsync(model);
                return Ok(tokens);
            }
            catch
            {
                // Log the exception for internal monitoring purposes (optional)
                return Unauthorized("Invalid email or password");
            }
        }

        // 🔹 Logout - Invalidate Token
        [HttpPost]
        [Authorize]
        public IActionResult Logout([FromBody] TokenDto tokenDto)
        {
            if (tokenDto == null || string.IsNullOrWhiteSpace(tokenDto.AccessToken))
                return BadRequest("Invalid token");

            _authService.Logout(tokenDto.AccessToken);
            return Ok(new { Message = "Successfully logged out" });
        }

        // 🔹 Refresh Token - Get New Access Token
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> RefreshToken([FromBody] TokenDto tokenDto)
        {
            if (tokenDto == null || string.IsNullOrWhiteSpace(tokenDto.RefreshToken))
                return BadRequest("Invalid refresh token");

            var newToken = await _authService.RefreshTokenAsync(tokenDto.RefreshToken);
            if (newToken == null)
                return Unauthorized("Invalid or expired refresh token");

            return Ok(newToken);
        }
    }
}
