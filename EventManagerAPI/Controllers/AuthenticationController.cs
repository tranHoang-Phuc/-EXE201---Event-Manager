using EventManagerAPI.DTO.Request;
using EventManagerAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authService;

        public AuthenticationController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var response = await _authService.LoginUserAsync(request);
            if (response == null)
            {
                return Unauthorized(new { authenticated = false, message = "Invalid email or password." });
            }

            return Ok(response);
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var (isSuccess, message) = await _authService.RegisterUserAsync(request);

            if (!isSuccess)
            {
                return BadRequest(new { message });
            }

            return Ok(new { message });
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            var result = await _authService.ForgotPasswordAsync(request.Email);
            if (result.Succeeded)
            {
                return Ok(new { Message = "If the email exists, a password reset link has been sent." });
            }
            return BadRequest(new { Message = result.Message });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            var result = await _authService.ResetPasswordAsync(request.UserId, request.Token, request.NewPassword);
            if (result.Succeeded)
            {
                return Ok(new { Message = "Password reset successfully!" });
            }
            return BadRequest(new { Message = result.Message });
        }

   
        [HttpGet("validate-token")]
        [Authorize] 
        public IActionResult ValidateToken()
        {
            return Ok(new { isValid = true });
        }
    }
}
