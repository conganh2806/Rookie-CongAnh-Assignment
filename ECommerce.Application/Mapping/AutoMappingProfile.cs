using AutoMapper;
using ECommerce.Application.DTOs;
using ECommerce.Application.DTOs.Request;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Entities.ApplicationUser;

namespace ECommerce.Application.Mapping
{
    public class AutoMappingProfile : Profile
    {
        public AutoMappingProfile()
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<Product, ProductFeatureResponse>();
            CreateMap<Product, ProductDetailResponse>();
            CreateMap<ProductCreateRequest, Product>();
            CreateMap<ProductUpdateRequest, Product>();
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryCreateRequest, Category>();
            CreateMap<CategoryUpdateRequest, Category>();
            CreateMap<OrderCreateRequest, Order>();
            CreateMap<OrderUpdateRequest, Order>();
            CreateMap<Order, OrderDto>();
            CreateMap<UserDto, User>();
            CreateMap<UserUpdateRequest, User>();
            CreateMap<User, UserDto>();            
        }
    }
}