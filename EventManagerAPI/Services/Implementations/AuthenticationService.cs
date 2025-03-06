using EventManagerAPI.Constant;
using EventManagerAPI.DTO.Request;
using EventManagerAPI.DTO.Response;
using EventManagerAPI.Models;
using EventManagerAPI.Repository.Implementations;
using EventManagerAPI.Repository.Interfaces;
using EventManagerAPI.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EventManagerAPI.Services.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationRepository _authRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public AuthenticationService(IAuthenticationRepository authRepository, IEmailService emailService, UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _userManager = userManager;
            _emailService = emailService;
            _configuration = configuration;
        }


        public async Task<LoginResponse?> LoginUserAsync(LoginRequest request)
        {
            var user = await _authRepository.GetUserByEmailAsync(request.Email);
            if (user == null)
            {
                Console.WriteLine($"User not found for email: {request.Email}");
                return new LoginResponse { Authenticated = false, Token = string.Empty };
            }

            var isValidPassword = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!isValidPassword)
            {
                Console.WriteLine($"Invalid password for user: {request.Email}");
                return new LoginResponse { Authenticated = false, Token = string.Empty };
            }

            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.Count > 0 ? roles[0] : PredefineRole.User;
            var token = GenerateJwtToken(user, role);

            return new LoginResponse
            {
                Token = token,
                Authenticated = true
            };
        }


        private string GenerateJwtToken(AppUser user, string role)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, role) 
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SignerKey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2), // Token hết hạn sau 2 giờ
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    
        public async Task<(bool IsSuccess, string Message)> RegisterUserAsync(RegisterRequest request)
        {
            if (await _userManager.FindByEmailAsync(request.Email) != null)
            {
                return (false, "Email already exists.");
            }

            var user = new AppUser
            {
                UserName = request.Email,
                Email = request.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                return (false, $"User registration failed: {errors}");
            }

            await _userManager.AddToRoleAsync(user, PredefineRole.User);

            return (true, "User registered successfully!");
        }

        public async Task<ServiceResult> ForgotPasswordAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return ServiceResult.Failure("Email not found.");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetLink = $"https://eventset.online/reset-password?userId={user.Id}&token={Uri.EscapeDataString(token)}";

            await _emailService.SendPasswordResetEmailAsync(email, resetLink);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult> ResetPasswordAsync(string userId, string token, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return ServiceResult.Failure("User not found.");
            }

            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
            if (result.Succeeded)
            {
                return ServiceResult.Success();
            }

            return ServiceResult.Failure("Invalid token or password reset failed.");
        }
    }
}
