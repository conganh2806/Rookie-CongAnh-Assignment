using ECommerce.Domain.Entities.ApplicationUser;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using ECommerce.Infrastructure.Persistence;

namespace ECommerce.Infrastructure.Extensions
{
    public static partial class ExtensionMethods
    {
        public static IServiceCollection AddCustomIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<User, Role>()
                            .AddEntityFrameworkStores<ApplicationDbContext>()
                            .AddDefaultTokenProviders();

            return services;
        }
    }
}
