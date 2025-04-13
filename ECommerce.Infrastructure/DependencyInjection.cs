using ECommerce.Application.Domain.Interfaces;
using ECommerce.Application.DTOs.Common;
using ECommerce.Application.Interfaces;
using ECommerce.Application.Services.Authentication;
using ECommerce.Domain.Interfaces;
using ECommerce.ECommerce.Application.DTOs.Common;
using ECommerce.Infrastructure.Persistence;
using ECommerce.Infrastructure.Persistence.Seed;
using ECommerce.Infrastructure.Repositories;
using ECommerce.Infrastructure.Services;
using ECommerce.Infrastructure.Services.Payments;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Minio;
using Minio.Helper;
using StackExchange.Redis;
using VNPAY.NET;

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

            //Minio
            services.Configure<MinioSettings>(configuration.GetSection("MinioSettings"));

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

            //VNPay
            services.Configure<VNPaySettings>(configuration.GetSection("VnPay"));
            
            services.AddScoped<IMediaFileService, MediaFileService>();

            services.AddRepository(configuration);
            
            return services;
        }

        public static IServiceCollection AddSeeder(this IServiceCollection services, 
                                                            IConfiguration configuration)
        {
            services.AddTransient<UserSeeder>();
            services.AddTransient<RolesSeeder>();
            services.AddTransient<ProductSeeder>();
            services.AddTransient<CategorySeeder>();
            services.AddTransient<OrderSeeder>();
            services.AddScoped<ISeedService, SeedService>();

            return services;
        }

        private static IServiceCollection AddRepository(this IServiceCollection services, 
                                                            IConfiguration configuration)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMediaFileRepository, MediaFileRepository>();

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
            // services.AddScoped<IPaymentService, VNPayService>();
            services.AddScoped<IVnpay, Vnpay>();

            return services;
        }
    }
}