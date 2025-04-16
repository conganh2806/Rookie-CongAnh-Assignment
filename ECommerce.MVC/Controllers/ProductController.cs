using ECommerce.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using YourProjectNamespace.Controllers;

namespace ECommerce.MVC.Controllers
{
    [Route("products")]
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService, ILogger<ProductController> logger) 
            : base(logger)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index(string categoryId)
        {
            var products = await _productService.GetByCategoryAsync(categoryId);
            return View(products);
        }

        [HttpGet("categories")]
        public async Task<IActionResult> GetProductsByCategory(string categoryId)
        {
            var products = await _productService.GetByCategoryAsync(categoryId);
            return PartialView("Partials/ProductList", products);
        }

        [Route("{slug}-i{id}")]
        public async Task<IActionResult> Detail(string slug, string id)
        {
            var product = await _productService.GetDetails(id);

            return View("ProductDetail", product);
        }
    }   
}
