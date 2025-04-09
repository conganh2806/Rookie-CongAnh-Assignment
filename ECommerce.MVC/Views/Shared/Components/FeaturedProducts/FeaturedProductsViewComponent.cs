using ECommerce.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Mvc.Views.Shared.Components.FeaturedProducts;

public class FeaturedProductsViewComponent : ViewComponent
{
    private readonly IProductService _productService;

    public FeaturedProductsViewComponent(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var products = await _productService.GetFeaturedProductsAsync();
        return View(products);
    }
}
