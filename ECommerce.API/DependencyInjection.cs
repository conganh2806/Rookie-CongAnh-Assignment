using ECommerce.Application.Interfaces;
using ECommerce.Application.Services.Authentication;
using ECommerce.Application.Settings;
using ECommerce.Infrastructure.Identity;

namespace ECommerce.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAPIService(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUser, CurrentUser>();
            services.AddScoped<IJWTAuthService, JWTAuthService>();
            services.Configure<JwtSettings>(configuration.GetSection("Jwt"));

            return services;
        }
    }
}
