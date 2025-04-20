using ECommerce.Application.DTOs.Request;
using ECommerce.Application.Interfaces;
using ECommerce.Application.Services;
using ECommerce.Domain.Enum;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.MVC.Controllers
{
    public class CheckoutController : BaseController
    {
        private readonly IRedisCartService _cartService;
        private readonly IOrderService _orderService;

        public CheckoutController(IRedisCartService cartService, 
                                IOrderService orderService,
                                ILogger<CheckoutController> logger)
                : base(logger)
        {
            _cartService = cartService;
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmPayment()
        {
            var userId = GetUserId();

            if(userId is null)
            {
                //return not found
            }

            var cart = await _cartService.GetCartAsync(userId);
            if (cart == null || !cart.Items.Any())
            {
                TempData["Error"] = "Cart is empty.";
                return RedirectToAction("Index", "Cart");
            }

            var orderRequest = new OrderCreateRequest
            {
                UserId = userId,
                TotalAmount = cart.Items.Sum(x => x.Price * x.Quantity),
                PaymentStatus = PaymentStatus.Pending,
                PaymentType = PaymentType.CashOnDelivery,
                Status = OrderStatus.Processing,
                Note = "Purchase order at " + DateTime.Now.ToString("g"),
                
            };

            await _orderService.CreateAsync(orderRequest);
            await _cartService.ClearCartAsync(userId);

            TempData["Success"] = "Order has been placed successfully!";
            return RedirectToAction("Success");
        }

        public IActionResult Success() => View();
    }

}