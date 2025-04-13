using AutoMapper;
using AutoMapper.QueryableExtensions;
using ECommerce.Application.DTOs;
using ECommerce.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Services.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IConfigurationProvider _config;
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _config = _mapper.ConfigurationProvider;
        }

        public async Task<List<CategoryDto>> GetAllAsync()
        {
             return await _categoryRepository.Ts.
                        ProjectTo<CategoryDto>(_config)
                        .AsNoTracking()
                        .ToListAsync();
        }
    }
}