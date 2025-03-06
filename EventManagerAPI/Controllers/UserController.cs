using AutoMapper;
using EventManagerAPI.DTO.Request;
using EventManagerAPI.DTO.Response;
using EventManagerAPI.Models;
using EventManagerAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EventManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Chỉ cho phép user đã đăng nhập
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound(new { Message = "User not found." });

            return Ok(user);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpPost]
        [AllowAnonymous] // Cho phép tạo user mà không cần đăng nhập
        public async Task<IActionResult> CreateUser([FromBody] AppUser user, [FromQuery] string password)
        {
            if (string.IsNullOrEmpty(password))
                return BadRequest(new { Message = "Password is required." });

            var success = await _userService.CreateUserAsync(user, password);
            if (!success)
                return BadRequest(new { Message = "User creation failed." });

            return Ok(new { Message = "User created successfully." });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] AppUser user)
        {
            if (id != user.Id)
                return BadRequest(new { Message = "User ID mismatch." });

            var success = await _userService.UpdateUserAsync(user);
            if (!success)
                return BadRequest(new { Message = "User update failed." });

            return Ok(new { Message = "User updated successfully." });
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var success = await _userService.DeleteUserAsync(id);
            if (!success)
                return NotFound(new { Message = "User not found or deletion failed." });

            return Ok(new { Message = "User deleted successfully." });
        }


        [HttpGet("myInfo")]
        [Authorize]
        public async Task<IActionResult> GetMyInfo()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { Message = "User not authenticated." });

            var user = await _userService.GetCurrentUserInfoAsync(userId);
            if (user == null)
                return NotFound(new { Message = "User not found." });

            return Ok(user);
        }
    }
}
