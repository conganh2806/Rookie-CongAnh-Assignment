using AutoMapper;
using AutoMapper.QueryableExtensions;
using ECommerce.Application.Common.Utilities.Exceptions;
using ECommerce.Application.DTOs;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Services
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
            var categories = await _categoryRepository.Entity.
                        ProjectTo<CategoryDto>(_config)
                        .AsNoTracking()
                        .ToListAsync();

            var result = categories.Where(c => c.ParentId == null).ToList();
            foreach (var parentCategory in result)
            {
                parentCategory.SubCategories = GetSubCategories(categories, parentCategory.Id);
            }

            return result;    
        }

        private List<CategoryDto> GetSubCategories(List<CategoryDto> categories, string? parentId)
        {
            var subcategories = categories.Where(c => c.ParentId == parentId).ToList();

            foreach (var subcategory in subcategories)
            {
                subcategory.SubCategories = GetSubCategories(categories, subcategory.Id);
            }

            return subcategories;
        }

        public async Task<CategoryDto?> GetByIdAsync(string id)
        {
            return await _categoryRepository.Entity.Where(c => c.Id == id)
                        .ProjectTo<CategoryDto>(_config)
                        .AsNoTracking()
                        .FirstOrDefaultAsync();
        }

        public async Task<CategoryDto?> GetBySlugAsync(string slug)
        {
            return await _categoryRepository.Entity.Where(c => c.Slug == slug)
                        .ProjectTo<CategoryDto>(_config)
                        .AsNoTracking()
                        .FirstOrDefaultAsync();
        }

        public async Task<CategoryDto> CreateAsync(CategoryCreateRequest request)
        {
            if(!string.IsNullOrWhiteSpace(request.ParentId))
            {
                var parentCategory = await _categoryRepository.Entity
                                            .FirstOrDefaultAsync(c => c.Id == request.ParentId);
                if (parentCategory == null) 
                {
                    throw new NotFoundException(
                        $"Parent category with id {request.ParentId} not found."
                    );
                }
            }

            var category = _mapper.Map<Category>(request);
            _categoryRepository.Add(category);
            await _categoryRepository.UnitOfWork.SaveChangesAsync();

            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto> UpdateAsync(string id, CategoryUpdateRequest request)
        {
            var category = await _categoryRepository.Entity
                        .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                throw new NotFoundException($"Category with id {id} not found.");
            }

            if (!string.IsNullOrWhiteSpace(request.ParentId))
            {
                var parentCategory = await _categoryRepository.Entity
                    .FirstOrDefaultAsync(c => c.Id == request.ParentId);

                if (parentCategory == null)
                {
                    throw new NotFoundException
                    (
                        $"Parent category with id {request.ParentId} not found."
                    );
                }
            }

            _mapper.Map(request, category);
            _categoryRepository.Update(category);
            await _categoryRepository.UnitOfWork.SaveChangesAsync();

            return _mapper.Map<CategoryDto>(category);
        }

        public async Task DeleteAsync(string id)
        {
            var category = await _categoryRepository.Entity.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null) 
            {
               throw new NotFoundException($"Category with id {id} not found.");
            }

            if (await _categoryRepository.Entity.AnyAsync(c => c.ParentId == category.Id))
            {
                throw new InvalidOperationException(
                    "Cannot delete a category that has subcategories."
                    );
            }

            _categoryRepository.Delete(category);
            await _categoryRepository.UnitOfWork.SaveChangesAsync();
        }
    }
}