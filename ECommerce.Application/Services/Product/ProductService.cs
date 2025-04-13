using AutoMapper;
using AutoMapper.QueryableExtensions;
using ECommerce.Application.Common.Utilities.Exceptions;
using ECommerce.Application.Domain.Interfaces;
using ECommerce.Application.DTOs;
using ECommerce.Application.DTOs.Request;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IConfigurationProvider _config;

        public ProductService(IProductRepository productRepository, 
                                ICategoryRepository categoryRepository,
                                IMapper mapper)        
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _config = _mapper.ConfigurationProvider;
        }

        public async Task<List<ProductDTO>> GetAllAsync()
        {
            return await _productRepository.Entity.ProjectTo<ProductDTO>(_config)
                            .AsNoTracking()
                            .ToListAsync();
        }

        public async Task<List<ProductDTO>> GetByCategoryAsync(string categoryId)
        {
            return await _productRepository.Entity.Where(p => p.Categories
                                                    .Any(c => c.Id == categoryId))
                                                    .ProjectTo<ProductDTO>(_config)
                                                    .AsNoTracking()
                                                    .ToListAsync();
        }

        public async Task<ProductDTO?> GetByIdAsync(string id)
        {
            return await _productRepository.Entity.Where(p => p.Id == id)
                                                    .ProjectTo<ProductDTO>(_config)
                                                    .AsNoTracking()
                                                    .FirstOrDefaultAsync();  
        }

        public async Task<ProductDetailResponse?> GetDetails(string id)
        {
            var product = await _productRepository.Entity.Include(p => p.Categories)
                                                    .Where(p => p.Id == id)                                                    
                                                    .FirstOrDefaultAsync();
        
            if (product == null)
            {
                throw new NotFoundException($"Product with id {id} not found.");
            }

            var categoryNames = product.Categories.Select(c => c.Name)
                                                    .Where(name => name != null)   
                                                    .Select(name => name!)  
                                                    .ToList();
                                                    
            var productDetail = _mapper.Map<ProductDetailResponse>(product);
            productDetail.CategoryNames = categoryNames;

            return productDetail;
        }

        public async Task<List<ProductDTO>> GetBySlugAsync(string slug)
        {
            return await _productRepository.Entity.Where(p => p.Slug == slug)
                                                    .ProjectTo<ProductDTO>(_config)
                                                    .AsNoTracking()
                                                    .ToListAsync(); 
        }

        public async Task<ProductDTO> CreateAsync(ProductCreateRequest request)
        {
            var categories = await _categoryRepository.Entity
                                            .Where(c => request.CategoryIds.Contains(c.Id))
                                            .ToListAsync();
                                            
            var product = _mapper.Map<Product>(request);
            product.Categories = categories;
            // product.Slug = product.Name.GenerateSlug();
            
            _productRepository.Add(product);
            await _productRepository.UnitOfWork.SaveChangesAsync();

            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<ProductDTO> UpdateAsync(string id, ProductUpdateRequest request)
        {
            var product = await _productRepository.Entity
                                            .Include(p => p.Categories)
                                            .FirstOrDefaultAsync(p => p.Id == id);

            if(product == null) 
            {
                throw new NotFoundException($"Product with id {id} not found.");
            }

            if (request.CategoryIds == null || !request.CategoryIds.Any())
            {
                throw new BadRequestException("CategoryIds cannot be null or empty.");
            }

            product = _mapper.Map(request, product);
            var categories = await _categoryRepository.Entity.Where(c => 
                                    request.CategoryIds.Contains(c.Id)).ToListAsync();

            product.Categories = categories;
            _productRepository.Update(product);
            await _productRepository.UnitOfWork.SaveChangesAsync();
            
            return _mapper.Map<ProductDTO>(product);
        }

        public async Task DeleteAsync(string id)
        {
            var product = await _productRepository.Entity.FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                throw new NotFoundException($"Product with id {id} not found.");
            }

            _productRepository.Delete(product);
            await _productRepository.UnitOfWork.SaveChangesAsync();
        }

        public async Task<List<ProductFeatureResponse>> GetFeaturedAsync(int take)
        {
            return await _productRepository.Entity.Where(p => p.IsFeatured)
                                                    .ProjectTo<ProductFeatureResponse>(_config)
                                                    .AsNoTracking()
                                                    .OrderBy(p => p.Price)
                                                    .Take(take)
                                                    .ToListAsync();
        }

        public async Task<List<ProductDTO>> SearchProductsAsync(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword)) 
            {
                return new List<ProductDTO>();
            }

            return await _productRepository.Entity.Where(p =>
                            (p.Name != null && p.Name.Contains(keyword)) ||
                            (p.Description != null && p.Description.Contains(keyword)))
                        .ProjectTo<ProductDTO>(_config)
                        .ToListAsync();
        }
    }
}

