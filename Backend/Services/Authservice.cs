using MediCare.Data;
using MediCare.DTOs;
using MediCare.Models;
using MediCare_.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using static QuestPDF.Helpers.Colors;
namespace MediCare.Services
{
    public interface IAuthService
    {
        Task<TokenResponseDto> LoginAsync(AuthDTOs loginDto);
        void Logout(string token);
        Task<TokenResponseDto> RefreshTokenAsync(string refreshToken);
        bool IsTokenRevoked(string token);
    }

    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ICacheService _cacheService;

        public AuthService(ApplicationDbContext context, IConfiguration configuration, ICacheService cacheService)
        {
            _context = context;
            _configuration = configuration;
            _cacheService = cacheService;
        }

        public async Task<TokenResponseDto> LoginAsync([FromBody] AuthDTOs loginDto)
        {
            var user = await _context.Users
                                      .Include(u => u.Role)
                                      .FirstOrDefaultAsync(u => u.Email == loginDto.Email);

            if (user == null || !VerifyPassword(loginDto.Password, user.Password))
                throw new UnauthorizedAccessException("Invalid email or password");

            var accessToken = GenerateJwtToken(user);
            var refreshToken = GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _context.SaveChangesAsync();

            return new TokenResponseDto { AccessToken = accessToken, RefreshToken = refreshToken };
        }

        public void Logout(string token)
        {
            // Store the invalidated token in cache with a short expiration time (e.g., 30 minutes)
            _cacheService.Set(token, "invalid", TimeSpan.FromMinutes(30));
    
            //Console.WriteLine(_cacheService.Get(token));

        }

        public async Task<TokenResponseDto> RefreshTokenAsync(string refreshToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);

            if (user == null || user.RefreshTokenExpiryTime < DateTime.UtcNow)
                return null;

            var newAccessToken = GenerateJwtToken(user);
            var newRefreshToken = GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(3);
            await _context.SaveChangesAsync();

            return new TokenResponseDto { AccessToken = newAccessToken, RefreshToken = newRefreshToken };
        }

        private string GenerateJwtToken(User user)
        {
            var secretKey = _configuration["JwtSettings:SecretKey"];
            var issuer = _configuration["JwtSettings:Issuer"];
            var audience = _configuration["JwtSettings:Audience"];
            var expirationTime = DateTime.UtcNow.AddMinutes(1);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Role.RoleName),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(expirationTime).ToUnixTimeSeconds().ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expirationTime,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            var randomBytes = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }
            return Convert.ToBase64String(randomBytes);
        }

        public bool VerifyPassword(string enteredPassword, string storedPasswordHash)
        {
            return BCrypt.Net.BCrypt.Verify(enteredPassword, storedPasswordHash);
        }

        public bool IsTokenRevoked(string token)
        {
            // Check if the token is in cache

            //Console.WriteLine($"Sent token Value: {token}, Type: {token.GetType()}");
            //Console.WriteLine(_cacheService.Contains(token));
            return _cacheService.Contains(token);
        }
    }

}