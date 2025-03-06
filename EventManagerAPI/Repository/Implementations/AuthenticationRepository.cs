using EventManagerAPI.Models;
using EventManagerAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EventManagerAPI.Repository.Implementations
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly UserManager<AppUser> _userManager;

        public AuthenticationRepository(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public Task AddUserAsync(AppUser user)
        {
            throw new NotImplementedException();
        }


        public Task<AppUser> GetUserByEmailAndPasswordAsync(string email, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<AppUser> GetUserByEmailAsync(string email)
        {
            return await _userManager.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public Task UpdateUserAsync(AppUser user)
        {
            throw new NotImplementedException();
        }
    }
}
