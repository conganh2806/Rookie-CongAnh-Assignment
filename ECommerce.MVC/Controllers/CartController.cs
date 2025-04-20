using AspNetCoreGeneratedDocument;
using ECommerce.Application.Common.Utilities.Exceptions;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace ECommerce.MVC.Controllers
{
    public class CartController : BaseController
    {
        private readonly IRedisCartService _redisCartService;
        private readonly IProductService _productService;

        public CartController(IRedisCartService redisCartService,
                                IProductService productService,
                                ILogger<CartController> logger)
            : 
            base(logger)
        {
            _productService = productService;
            _redisCartService = redisCartService;
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartRequest request)
        {
            var userId = GetUserId();

            if (string.IsNullOrEmpty(userId))
            {
                return Json(new 
                {
                    success = false, 
                    message = "You need to login to add product to cart" 
                });
            }

            var cartItem = new CartItemDto
            {
                ProductId = request.ProductId,
                Quantity = request.Quantity,
            };

            await _redisCartService.AddOrUpdateItemAsync(userId, cartItem);

            return Json(new 
            {
                success = true,
                message = "Product is added to cart!"
            });
        }

        public async Task<IActionResult> ViewCart()
        {
            var userId = GetUserId();

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account"); 
            }

            var cart = await _redisCartService.GetCartAsync(userId);

            return View(cart);  
        }

        [HttpGet]
        public async Task<IActionResult> GetCartItemCount()
        {
            var userId = GetUserId();
            
            if (string.IsNullOrEmpty(userId)) 
            {
                return Json(new 
                {
                    count = 0
                });
            }

            var cart = await _redisCartService.GetCartAsync(userId);
            var totalItems = cart?.Items.Sum(i => i.Quantity) ?? 0;
            
            return Json(new { 
                count = totalItems 
            });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(string productId)
        {
            var userId = GetUserId();

            if(userId is null)
            { 
                throw new NotFoundException("User not found");
            }

            await _redisCartService.RemoveItemAsync(userId, productId);

            TempData["SuccessMessage"] = "Product has been removed from your cart.";

            return RedirectToAction("ViewCart");
        }
    }
}
