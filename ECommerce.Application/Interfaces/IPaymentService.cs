using ECommerce.Application.DTOs;
using Microsoft.AspNetCore.Http;

namespace ECommerce.Application.Interfaces
{
    public interface IPaymentService
    {
        Task<string> CreatePaymentUrlAsync(OrderDto order, HttpContext httpContext);
        Task<PaymentResult> ProcessPaymentCallbackAsync(HttpRequest request);
    }
}