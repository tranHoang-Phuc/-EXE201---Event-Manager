using EventManagerAPI.DTO.Request;
using EventManagerAPI.DTO.Response;
using EventManagerAPI.Models;

namespace EventManagerAPI.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<LoginResponse?> LoginUserAsync(LoginRequest request);
        Task<(bool IsSuccess, string Message)> RegisterUserAsync(RegisterRequest request);
        Task<ServiceResult> ForgotPasswordAsync(string email);
        Task<ServiceResult> ResetPasswordAsync(string userId, string token, string newPassword);
    }
}
