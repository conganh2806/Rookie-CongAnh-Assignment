using System.ComponentModel.DataAnnotations;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Enum;

namespace ECommerce.Application.DTOs.Request
{
    public class OrderCreateRequest
    {
        [Required(ErrorMessage = "User ID is required.")]
        [StringLength(450, MinimumLength = 1)]
        public string UserId { get; set; } = default!;

        [Required(ErrorMessage = "Total amount is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Total amount must be greater than 0.")]
        public decimal TotalAmount { get; set; }

        [Required(ErrorMessage = "Payment type is required.")]
        [EnumDataType(typeof(PaymentType), ErrorMessage = "Invalid payment type.")]
        public PaymentType PaymentType { get; set; }

        [Required(ErrorMessage = "Payment status is required.")]
        [EnumDataType(typeof(PaymentStatus), ErrorMessage = "Invalid payment status.")]
        public PaymentStatus PaymentStatus { get; set; }

        [Required(ErrorMessage = "Order status is required.")]
        [EnumDataType(typeof(OrderStatus), ErrorMessage = "Invalid order status.")]
        public OrderStatus Status { get; set; }
        public string? Note { get; set; }
        public List<OrderDetail> OrderDetail { get; set; } = new List<OrderDetail>();
    }
}