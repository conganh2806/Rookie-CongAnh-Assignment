using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ECommerce.Application.Settings;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ECommerce.Infrastructure.Extensions
{
    public static partial class ExtensionMethods
    {
        public static IServiceCollection AddJWTAuthentication(this IServiceCollection services,
                                                                        IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("Jwt").Get<JwtSettings>();

            services.AddAuthentication(options => { 
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer  = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new 
                                SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                };
            });

            return services;
        }

        public static IServiceCollection AddCookieAuthentication(this IServiceCollection services, 
                                                                        IConfiguration configuration)
        {
            services.AddAuthentication(options => {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(options => {
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Account/AccessDenied";
            });

            return services;
        }

        public static IServiceCollection AddCustomAuthorization(this IServiceCollection services, 
                                                                    IConfiguration configuration)
        {
            services.AddAuthorization(options => {
                options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
                
                options.AddPolicy("UserPolicy", policy => policy.RequireRole("User"));
            });

            return services;
        }
    }
}