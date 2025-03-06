using EventManagerAPI.Models;
using EventManagerAPI.Repository.Interfaces;
using EventManagerAPI.Services.Interfaces;

namespace EventManagerAPI.Services.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<Role> GetRoleByIdAsync(string roleId)
        {
            return await _roleRepository.GetRoleByIdAsync(roleId);
        }

        public async Task<Role> GetRoleByNameAsync(string roleName)
        {
            return await _roleRepository.GetRoleByNameAsync(roleName);
        }

        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            return await _roleRepository.GetAllRolesAsync();
        }

        public async Task<bool> CreateRoleAsync(Role role)
        {
            return await _roleRepository.AddRoleAsync(role);
        }

        public async Task<bool> UpdateRoleAsync(Role role)
        {
            return await _roleRepository.UpdateRoleAsync(role);
        }

        public async Task<bool> DeleteRoleAsync(string roleId)
        {
            return await _roleRepository.DeleteRoleAsync(roleId);
        }

        public async Task<bool> AssignRoleToUserAsync(string userId, string roleName)
        {
            return await _roleRepository.AddUserToRoleAsync(userId, roleName);
        }

        public async Task<bool> RemoveRoleFromUserAsync(string userId, string roleName)
        {
            return await _roleRepository.RemoveUserFromRoleAsync(userId, roleName);
        }
    }
}
