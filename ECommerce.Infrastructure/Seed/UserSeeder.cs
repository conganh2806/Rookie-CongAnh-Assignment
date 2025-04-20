using ECommerce.Domain.Entities.ApplicationUser;
using Microsoft.AspNetCore.Identity;
using BCrypt.Net;
using ECommerce.Domain.Interfaces;

namespace ECommerce.Infrastructure.Persistence.Seed
{
    public class UserSeeder
    {
        private readonly IUserRepository _userRepository;

        public UserSeeder(IUserRepository userRepository,
                            RoleManager<IdentityRole> roleManager)
        {
            _userRepository = userRepository;
        }

        public async Task SeedAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
           var adminUser = await userManager.FindByEmailAsync("admin@example.com");
            
            if (adminUser is null)
            {
                adminUser = new User
                {
                    UserName = "admin@example.com",
                    Email = "admin@example.com",
                    FirstName = "Admin",
                    LastName = "User",
                    EmailConfirmed = true,
                };
            
                var result = await userManager.CreateAsync(adminUser);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "User");
                }
            }

            var normalUser = await userManager.FindByEmailAsync("user@example.com");

            if (normalUser is null)
            {
                normalUser = new User
                {
                    UserName = "user@example.com",
                    Email = "user@example.com",
                    FirstName = "Normal",
                    LastName = "User",
                    EmailConfirmed = true,
                };

                var result = await userManager.CreateAsync(normalUser, "Password123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(normalUser, "User");
                }
            }
        }
    }
}
