using ECommerce.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Mvc.ViewComponents
{
    public class FeaturedProductsViewComponent : ViewComponent
    {
        private readonly IProductService _productService;

        public FeaturedProductsViewComponent(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var featuredProducts = await _productService.GetFeaturedAsync(4);

            return View(featuredProducts);
        }
    }
}

