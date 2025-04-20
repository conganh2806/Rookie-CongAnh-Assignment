using ECommerce.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.MVC.Controllers
{
    public class SearchController : BaseController
    { 
        private readonly IProductService _productService;

        public SearchController(IProductService productService, ILogger<ProductController> logger) 
            : base(logger)
        {
            _productService = productService;
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchProducts(string query)
        {
            var results = await _productService.SearchProductsAsync(query);
            
            return View(results);
        }
    }
}