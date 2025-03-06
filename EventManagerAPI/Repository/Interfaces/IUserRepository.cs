using EventManagerAPI.Models;

namespace EventManagerAPI.Repository.Interfaces
{
	public interface IUserRepository
	{
        Task<AppUser> GetUserByIdAsync(string userId);
        Task<AppUser> GetUserByEmailAsync(string email);
        Task<IEnumerable<AppUser>> GetAllUsersAsync();
        Task<bool> AddUserAsync(AppUser user);
        Task<bool> UpdateUserAsync(AppUser user);
        Task<AppUser> GetCurrentUserInfoAsync(string userId); 
        Task<bool> DeleteUserAsync(string userId);
    }
}
