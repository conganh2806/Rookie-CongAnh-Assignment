using AutoMapper;

namespace ECommerce.Application.Mapping
{
    public class AutoMappingProfile : Profile
    {
        public AutoMappingProfile()
        {
            // CreateMap<Product, ProductDto>()
            //     .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

        }
    }
}