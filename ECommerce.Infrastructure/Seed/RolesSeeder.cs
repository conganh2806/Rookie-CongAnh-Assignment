using ECommerce.Domain.Entities.ApplicationUser;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Infrastructure.Persistence.Seed
{
    public static class RolesSeeder
    {
        public static async Task SeedAsync(UserManager<User> userManager, 
                                        RoleManager<IdentityRole> roleManager)
        {
            string[] roleNames = { "Admin", "User", "Manager" };

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }
    }
}
