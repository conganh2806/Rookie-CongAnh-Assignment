using Moq;
using AutoMapper;
using ECommerce.Application.Services;
using ECommerce.Domain.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Application.DTOs;
using ECommerce.Application.Domain.Interfaces;
using ECommerce.Application.DTOs.Request;

namespace ECommerce.Tests.Services.Products
{
    public partial class ProductServiceTests
    {
        private readonly ProductService _productService;
        private readonly Mock<IProductRepository> _mockProductRepo;
        private readonly Mock<ICategoryRepository> _mockCategoryRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;

        public ProductServiceTests()
        {
            _mockProductRepo = new Mock<IProductRepository>();
            _mockCategoryRepo = new Mock<ICategoryRepository>();
            _mockMapper = new Mock<IMapper>();

            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockUnitOfWork.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
                    .ReturnsAsync(1);

            _mockProductRepo.Setup(r => r.UnitOfWork)
                .Returns(_mockUnitOfWork.Object);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductDTO>().ForMember(dest => dest.CategoryNames,
                            opt => opt.MapFrom(src => src.Categories.Select(c => c.Name)));
                cfg.CreateMap<Product, ProductDetailResponse>();
                cfg.CreateMap<ProductCreateRequest, Product>();
                cfg.CreateMap<ProductUpdateRequest, Product>();
                cfg.CreateMap<Product, ProductFeatureResponse>();
            });

            var mapper = new Mapper(config);

            _productService = new ProductService(
                _mockProductRepo.Object,
                _mockCategoryRepo.Object,
                mapper
            );
        }
    }
}