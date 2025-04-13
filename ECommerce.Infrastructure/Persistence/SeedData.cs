using Microsoft.Extensions.DependencyInjection;
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
        }
    }
}
