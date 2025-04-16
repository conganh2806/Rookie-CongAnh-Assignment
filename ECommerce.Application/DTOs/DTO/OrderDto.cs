using ECommerce.Domain.Enum;

namespace ECommerce.Application.DTOs
{
    public class OrderDto
    {
        public string Id { get; set; } = default!;
        public string UserId { get; set; } = default!;
        public decimal TotalAmount { get; set; }
        public PaymentType PaymentType { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public OrderStatus Status { get; set; }
        public string? Note { get; set; }
    }    
}