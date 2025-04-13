using ECommerce.Domain.Entities.ApplicationUser;
using Microsoft.AspNetCore.Identity;
using BCrypt.Net;
using ECommerce.Domain.Interfaces;

namespace ECommerce.Infrastructure.Persistence.Seed
{
    public class UserSeeder
    {
        private readonly IUserRepository _userRepository;

        public UserSeeder(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task SeedAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            var adminUser = await userManager.FindByEmailAsync("admin@example.com");

            if (adminUser == null)
            {
                adminUser = new User
                {
                    UserName = "admin@example.com",
                    Email = "admin@example.com",
                    FirstName = "Admin",
                    LastName = "User",
                    EmailConfirmed = true,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password123!")
                };

                _userRepository.Add(adminUser);
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }

            var normalUser = await userManager.FindByEmailAsync("user@example.com");

            if (normalUser == null)
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
