using ECommerce.Domain.Entities.ApplicationUser;
using ECommerce.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Infrastructure.Extensions
{
    public static partial class ExtensionMethods
    {
        public static IServiceCollection AddCustomIdentity(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services
                .AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}
