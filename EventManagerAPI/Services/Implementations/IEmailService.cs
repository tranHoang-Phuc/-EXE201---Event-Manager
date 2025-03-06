namespace EventManagerAPI.Services.Implementations
{
    public interface IEmailService
    {
        Task SendPasswordResetEmailAsync(string email, string resetLink);
    }
}

