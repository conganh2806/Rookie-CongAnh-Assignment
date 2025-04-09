using ECommerce.Application.Interfaces;
using ECommerce.Application.Mapping;
using ECommerce.Application.Services.Product;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
        services.AddAutoMapper(typeof(AutoMappingProfile));
        return services;
    }
}
