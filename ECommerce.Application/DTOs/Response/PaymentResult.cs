using ECommerce.Domain.Enum;

namespace ECommerce.Application.DTOs
{
    public class PaymentResult
    {
        public bool Success { get; set; }                 
        public string Message { get; set; } = default!;         
        public string? TransactionId { get; set; }       
        public string? OrderId { get; set; }             
        public decimal? Amount { get; set; }             
        public PaymentType? PaymentMethod { get; set; }       
        public DateTime? PaidAt { get; set; }             
    }
}