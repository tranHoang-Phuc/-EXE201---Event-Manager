using EventManagerAPI.Models;
using EventManagerAPI.Repository.Interfaces;
using EventManagerAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById(string id)
        {
            var role = await _roleService.GetRoleByIdAsync(id);
            if (role == null)
                return NotFound(new { Message = "Role not found." });

            return Ok(role);
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetRoleByName(string name)
        {
            var role = await _roleService.GetRoleByNameAsync(name);
            if (role == null)
                return NotFound(new { Message = "Role not found." });

            return Ok(role);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _roleService.GetAllRolesAsync();
            return Ok(roles);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] Role role)
        {
            var success = await _roleService.CreateRoleAsync(role);
            if (!success)
                return BadRequest(new { Message = "Role creation failed." });

            return Ok(new { Message = "Role created successfully." });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(string id, [FromBody] Role role)
        {
            if (id != role.Id)
                return BadRequest(new { Message = "Role ID mismatch." });

            var success = await _roleService.UpdateRoleAsync(role);
            if (!success)
                return BadRequest(new { Message = "Role update failed." });

            return Ok(new { Message = "Role updated successfully." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var success = await _roleService.DeleteRoleAsync(id);
            if (!success)
                return NotFound(new { Message = "Role not found or deletion failed." });

            return Ok(new { Message = "Role deleted successfully." });
        }

        [HttpPost("assign/{userId}/{roleName}")]
        public async Task<IActionResult> AssignRoleToUser(string userId, string roleName)
        {
            var success = await _roleService.AssignRoleToUserAsync(userId, roleName);
            if (!success)
                return BadRequest(new { Message = "Failed to assign role to user." });

            return Ok(new { Message = "Role assigned to user successfully." });
        }

        [HttpDelete("remove/{userId}/{roleName}")]
        public async Task<IActionResult> RemoveRoleFromUser(string userId, string roleName)
        {
            var success = await _roleService.RemoveRoleFromUserAsync(userId, roleName);
            if (!success)
                return BadRequest(new { Message = "Failed to remove role from user." });

            return Ok(new { Message = "Role removed from user successfully." });
        }
    }
}
