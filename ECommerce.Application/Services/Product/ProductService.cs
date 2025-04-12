using AutoMapper;
using ECommerce.Application.Domain.Interfaces;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductDTO>> GetAllAsync()
    {
        var products = await _productRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ProductDTO>>(products);
    }

    public async Task<IEnumerable<ProductDTO>> GetByCategoryAsync(string categoryId)
    {
        var products = await _productRepository.GetByCategoryAsync(categoryId);
        return _mapper.Map<IEnumerable<ProductDTO>>(products);
    }

    public async Task<ProductDTO?> GetByIdAsync(string id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        return product == null ? null : _mapper.Map<ProductDTO>(product);
    }

    public async Task<IEnumerable<ProductDTO>> GetBySlugAsync(string slug)
    {
        var products = await _productRepository.GetProductsBySlug(slug);
        return _mapper.Map<IEnumerable<ProductDTO>>(products);
    }
}
