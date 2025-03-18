namespace MediCare.DTOs
{
    public class AuthDTOs
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class TokenResponseDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }

    public class TokenDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
