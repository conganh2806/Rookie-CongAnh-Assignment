using ECommerce.Application.Interfaces;
using ECommerce.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService; 

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("featured")]
        public async Task<IActionResult> GetFeatured() =>
            Ok(await _productService.GetFeaturedProductsAsync());
        }
}