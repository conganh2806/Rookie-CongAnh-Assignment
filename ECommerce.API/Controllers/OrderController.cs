using System.Net;
using ECommerce.Application.DTOs;
using ECommerce.Application.DTOs.Request;
using ECommerce.Application.Services;
using ECommerce.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [ApiController]
    [Route("api/orders")]
    [Authorize(Roles = "Admin")]
    public class OrderController : BaseController<Order>
    { 
        private readonly IOrderService _orderService;

        public OrderController(ILogger<Order> logger, IOrderService orderService) 
                : base(logger)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _orderService.GetAllAsync();
            return Success(orders, "Get all orders successfully.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var order = await _orderService.GetByIdAsync(id);
            if (order == null) 
            {
                return Error("Order not found", HttpStatusCode.NotFound);
            }

            return Success(order, "Get order by id successfully.");
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderCreateRequest request)
        {
            if(!ModelState.IsValid) 
            {
                return Error("Error validate", HttpStatusCode.BadRequest);
            }

            var order = await _orderService.CreateAsync(request);
            if (order == null) 
            {
                return Error("Order not found", HttpStatusCode.NotFound);
            }
        
            return Success(order, "Create order successfully.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] OrderUpdateRequest request)
        {
            if(!ModelState.IsValid) 
            {
                return Error("Error validate", HttpStatusCode.BadRequest);
            }

            var order = await _orderService.UpdateAsync(id, request);
            if (order == null) return Error("Order not found", HttpStatusCode.NotFound);
        
            return Success(order, "Update order successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var order = await _orderService.GetByIdAsync(id);
            if (order == null) return Error("Order not found", HttpStatusCode.NotFound);

            await _orderService.DeleteAsync(id);
            return Success<Order?>(null, "Delete order successfully.");
        }
    }
}