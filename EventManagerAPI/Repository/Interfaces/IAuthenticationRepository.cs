using EventManagerAPI.Models;

namespace EventManagerAPI.Repository.Interfaces
{
    public interface IAuthenticationRepository
    {
        Task<AppUser> GetUserByEmailAndPasswordAsync(string email, string password);
        Task<AppUser?> GetUserByEmailAsync(string email);
        Task AddUserAsync(AppUser user);
        Task UpdateUserAsync(AppUser user);
    }
}
