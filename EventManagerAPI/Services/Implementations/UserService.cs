using EventManagerAPI.Models;
using EventManagerAPI.Repository.Interfaces;
using EventManagerAPI.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace EventManagerAPI.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<AppUser> _userManager;

        public UserService(IUserRepository userRepository, UserManager<AppUser> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task<AppUser> GetUserByIdAsync(string userId)
        {
            return await _userRepository.GetUserByIdAsync(userId);
        }

        public async Task<AppUser> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetUserByEmailAsync(email);
        }

        public async Task<IEnumerable<AppUser>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<bool> CreateUserAsync(AppUser user, string password)
        {
           
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Password is required.");

            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                throw new Exception($"User creation failed: {string.Join("; ", result.Errors.Select(e => e.Description))}");
            }

            return true;
        }

        public async Task<bool> UpdateUserAsync(AppUser user)
        {
            return await _userRepository.UpdateUserAsync(user);
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            return await _userRepository.DeleteUserAsync(userId);
        }

        public async Task<AppUser> GetCurrentUserInfoAsync(string userId)
        {
            var user = await _userRepository.GetCurrentUserInfoAsync(userId);
            if (user == null)
                throw new Exception("User not found.");
            return user;
        }
    }

}

