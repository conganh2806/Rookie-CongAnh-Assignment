using ECommerce.Application.Domain.Interfaces;
using ECommerce.Application.DTOs.Common;
using ECommerce.Application.Interfaces;
using ECommerce.Application.Services.Authentication;
using ECommerce.Domain.Interfaces;
using ECommerce.Infrastructure.Persistence;
using ECommerce.Infrastructure.Persistence.Seed;
using ECommerce.Infrastructure.Repositories;
using ECommerce.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Minio;
using StackExchange.Redis;

namespace ECommerce.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, 
                                                            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString));

            services.Configure<MinioSettings>(configuration.GetSection("MinioSettings"));
            
            //Minio
            services.AddSingleton(serviceProvider =>
            {
                var config = serviceProvider.GetRequiredService<IOptions<MinioSettings>>().Value;
                
                return new MinioClient()
                    .WithEndpoint(config.Endpoint)
                    .WithCredentials(config.AccessKey, config.SecretKey)
                    .WithSSL(config.UseSSL)
                    .Build();
            });

            //Redis
            services.AddSingleton<IConnectionMultiplexer>(sp => { 
                var configuration = ConfigurationOptions.Parse("localhost:6379", true);
                return ConnectionMultiplexer.Connect(configuration);
            });

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMediaFileRepository, MediaFileRepository>();
            
            services.AddTransient<UserSeeder>();
            services.AddTransient<RolesSeeder>();
            services.AddTransient<ProductSeeder>();
            services.AddTransient<CategorySeeder>();
            services.AddTransient<OrderSeeder>();
            services.AddScoped<ISeedService, SeedService>();

            services.AddScoped<IMediaFileService, MediaFileService>();
            
            return services;
        }

        public static IServiceCollection AddAPIService(this IServiceCollection services, 
                                                            IConfiguration configuration)
        {
            services.AddScoped<IJWTAuthService, JWTAuthService>();

            return services;
        }

        public static IServiceCollection AddMVCService(this IServiceCollection services, 
                                                            IConfiguration configuration) 
        { 
            services.AddScoped<ICookieAuthService, CookieAuthService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IRedisCartService, RedisCartService>();
            
            return services;
        }
    }
}