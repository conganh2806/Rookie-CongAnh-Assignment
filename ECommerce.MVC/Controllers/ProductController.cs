using System.Threading.Tasks;
using ECommerce.Application.Common.Utilities.Exceptions;
using ECommerce.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using YourProjectNamespace.Controllers;

namespace ECommerce.MVC.Controllers
{
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

        [HttpGet]
        public async Task<IActionResult> GetProductsByCategory(string categoryId)
        {
            var products = await _productService.GetByCategoryAsync(categoryId);
            return PartialView("Partials/ProductList", products);
        }

        [Route("product/{slug}-i{id}")]
        public async Task<IActionResult> Detail(string slug, string id)
        {
            var product = await _productService.GetDetails(id);

            return View("ProductDetail", product);
        }
    }   
}
