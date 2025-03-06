using EventManagerAPI.Configuration;
using EventManagerAPI.Services.Implementations;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace EventManagerAPI.Services.Interfaces
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendPasswordResetEmailAsync(string email, string resetLink)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("EventSet", _emailSettings.SenderEmail));
                message.To.Add(new MailboxAddress("", email));
                message.Subject = "Reset Your Password";

                message.Body = new TextPart("html")
                {
                    Text = $"<p>Please reset your password by clicking <a href='{resetLink}'>here</a>. This link will expire in 24 hours.</p>"
                };

                using var client = new SmtpClient();
                await client.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.SmtpPort, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_emailSettings.SenderEmail, _emailSettings.SenderPassword);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
                throw new Exception($"Failed to send password reset email: {ex.Message}");
            }
        }
    }
}
