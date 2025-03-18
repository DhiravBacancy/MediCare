using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace MediCare.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendResetPasswordEmailAsync(string userEmail, string resetLink)
        {
            var smtpClient = new SmtpClient(_configuration["EmailSettings:SmtpServer"])
            {
                Port = int.Parse(_configuration["EmailSettings:SmtpPort"]),
                Credentials = new NetworkCredential(
                    _configuration["EmailSettings:Email"],
                    _configuration["EmailSettings:Password"]),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_configuration["EmailSettings:Email"]),
                Subject = "Reset Your Password",
                Body = $"<h1>Click the link below to reset your password</h1><p><a href=\"{resetLink}\">Reset Password</a></p>",
                IsBodyHtml = true,
            };

            mailMessage.To.Add(userEmail);

            try
            {
                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                // Log the error (you can replace this with proper logging)
                Console.WriteLine($"Error sending email: {ex.Message}");
            }
        }
    }

    public interface IEmailService
    {
        Task SendResetPasswordEmailAsync(string userEmail, string resetLink);
    }
}
