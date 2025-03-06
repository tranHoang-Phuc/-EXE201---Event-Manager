using EventManagerAPI.Constant;
using EventManagerAPI.Models;
using EventManagerAPI.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace EventManagerAPI.Configuration
{
    public class ApplicationInitConfig : IApplicationInitConfigService
    {
        private readonly IServiceProvider _serviceProvider;

        public ApplicationInitConfig(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task applicationRunner()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();

                await SeedRoles(roleManager);
                await SeedAdmin(userManager);
            }
        }

        private async Task SeedRoles(RoleManager<Role> roleManager)
        {
            foreach (var role in PredefineRole.Roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new Role { Name = role, Description = $"Default role: {role}" }); 
                }
            }
        }

        private async Task SeedAdmin(UserManager<AppUser> userManager)
        {
            var adminEmail = "admin@example.com";
            var adminPassword = "admin";

            var existingAdmin = await userManager.FindByEmailAsync(adminEmail);
            if (existingAdmin == null)
            {
                var adminUser = new AppUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, PredefineRole.Admin);
                }
            }
            else
            {
                if (!await userManager.IsInRoleAsync(existingAdmin, PredefineRole.Admin))
                {
                    await userManager.AddToRoleAsync(existingAdmin, PredefineRole.Admin);
                }
            }
        }
    }
}