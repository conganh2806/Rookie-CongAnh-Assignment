using ECommerce.Application.DTOs;
using ECommerce.Application.Services;
using ECommerce.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ECommerce.API.Controllers
{
    [ApiController]
    [Route("api/category")]
    public class CategoryController : BaseController<Category>
    { 
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService, ILogger<Category> logger)
            : base(logger)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();

            return Success(categories, "Get all categories successfully.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null) return Error("Category not found", HttpStatusCode.NotFound);

            return Success(category, "Get category by id successfully.");
        }

        [HttpGet("slug/{slug}")]
        public async Task<IActionResult> GetBySlug(string slug)
        {
            var category = await _categoryService.GetBySlugAsync(slug);

            return Success(category, "Get category by slug successfully.");
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CategoryCreateRequest request)
        {
            if(!ModelState.IsValid) 
            {
                return Error("Error validate", HttpStatusCode.BadRequest);
            }
            var category = await _categoryService.CreateAsync(request);
            if (category == null) return Error("Category not found", HttpStatusCode.NotFound);
         
            return Success(category, "Create category successfully.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] CategoryUpdateRequest request)
        {
            if(!ModelState.IsValid)
            {
                return Error("Error validate", HttpStatusCode.BadRequest);
            }   
            var category = await _categoryService.UpdateAsync(id, request);
            if (category == null) return Error("Category not found", HttpStatusCode.NotFound);

            return Success(category, "Update category successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _categoryService.DeleteAsync(id);
            
            return Success<Category?>(null, "Delete category successfully.");
        }
    }
}