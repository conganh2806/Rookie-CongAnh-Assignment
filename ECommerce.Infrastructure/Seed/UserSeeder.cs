using ECommerce.Domain.Entities.ApplicationUser;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Infrastructure.Persistence.Seed
{
    public static class UserSeeder
    {
        public static async Task SeedAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            var user = await userManager.FindByEmailAsync("admin@example.com");

            if (user == null)
            {
                user = new User
                {
                    UserName = "admin@example.com",
                    Email = "admin@example.com",
                    FirstName = "Admin",
                    LastName = "User",
                    EmailConfirmed = true,
                };

                var result = await userManager.CreateAsync(user, "Password123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }

            user = await userManager.FindByEmailAsync("user@example.com");

            if (user == null)
            {
                user = new User
                {
                    UserName = "user@example.com",
                    Email = "user@example.com",
                    FirstName = "Normal",
                    LastName = "User",
                    EmailConfirmed = true,
                };

                var result = await userManager.CreateAsync(user, "Password123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "User");
                }
            }
        }
    }
}
