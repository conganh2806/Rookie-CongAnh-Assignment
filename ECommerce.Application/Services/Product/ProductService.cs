using AutoMapper;
using AutoMapper.QueryableExtensions;
using ECommerce.Application.Domain.Interfaces;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly IConfigurationProvider _config;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _config = _mapper.ConfigurationProvider;
    }

    public async Task<List<ProductDTO>> GetAllAsync()
    {
        return await _productRepository.Ts.
                        ProjectTo<ProductDTO>(_config)
                        .AsNoTracking()
                        .ToListAsync();
    }

    public async Task<List<ProductDTO>> GetByCategoryAsync(string categoryId)
    {
        return await _productRepository.Ts
                            .Where(p => p.CategoryId == categoryId)
                            .ProjectTo<ProductDTO>(_config)
                            .AsNoTracking()
                            .ToListAsync();
    }

    public async Task<ProductDTO?> GetByIdAsync(string id)
    {
        return await _productRepository.Ts.Where(p => p.Id == id)
                                                .ProjectTo<ProductDTO>(_config)
                                                .AsNoTracking()
                                                .FirstOrDefaultAsync();  
    }

    public async Task<List<ProductDTO>> GetBySlugAsync(string slug)
    {
        return await _productRepository.Ts.Where(p => p.Slug == slug)
                                                .ProjectTo<ProductDTO>(_config)
                                                .AsNoTracking()
                                                .ToListAsync(); 
    }
}
