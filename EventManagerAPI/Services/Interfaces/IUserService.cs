using EventManagerAPI.Models;

namespace EventManagerAPI.Services.Interfaces
{
    public interface IUserService
    {

        Task<AppUser> GetUserByIdAsync(string userId);

        Task<AppUser> GetUserByEmailAsync(string email);
        Task<IEnumerable<AppUser>> GetAllUsersAsync();
        Task<bool> CreateUserAsync(AppUser user, string password);
        Task<bool> UpdateUserAsync(AppUser user);
        Task<bool> DeleteUserAsync(string userId);
        Task<AppUser> GetCurrentUserInfoAsync(string userId);

    }
}

