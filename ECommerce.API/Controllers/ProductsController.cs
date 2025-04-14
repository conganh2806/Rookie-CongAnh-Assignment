using System.Net;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseController<Product>
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService, ILogger<Product> logger)
                : base(logger)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllAsync();
            return Success(products, "Get all products successfully.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return Error("Product not found", HttpStatusCode.NotFound);
            return Success(product, "Get product successfully.");
        }

        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetByCategory(string categoryId)
        {
            var products = await _productService.GetByCategoryAsync(categoryId);
            return Success(products, "Get products by category successfully.");
        }

        [HttpGet("slug/{slug}")]
        public async Task<IActionResult> GetBySlug(string slug)
        {
            var products = await _productService.GetBySlugAsync(slug);
            return Success(products, "Get products by slug successfully.");
        }
    }
}
