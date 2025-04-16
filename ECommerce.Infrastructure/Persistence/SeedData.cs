using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ECommerce.Infrastructure.Persistence.Seed;
using ECommerce.Domain.Entities.ApplicationUser;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Infrastructure.Persistence
{
    public static class SeedData
    {
        public static async Task Initialize(this IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            using var scope = serviceProvider.CreateScope();
            var services = scope.ServiceProvider;
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            await UserSeeder.SeedAsync(userManager, roleManager);
            await CategorySeeder.SeedAsync(context);
            await ProductSeeder.SeedAsync(context);
            await OrderSeeder.SeedAsync(context);
        }
    }
}
