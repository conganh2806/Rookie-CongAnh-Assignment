using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.MVC.Controllers
{
    public class CartController : BaseController
    {
        private readonly IRedisCartService _redisCartService;

        public CartController(IRedisCartService redisCartService,
                                ILogger<CartController> logger)
            : 
            base(logger)
        {
            _redisCartService = redisCartService;
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(string productId, int quantity)
        {
            var userId = User.Identity?.Name;  

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
                ProductId = productId,
                Quantity = quantity
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
            var userId = User.Identity?.Name;

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
            var userId = User.Identity?.Name;
            
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
    }
}
