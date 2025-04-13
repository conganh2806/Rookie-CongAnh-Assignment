namespace ECommerce.Application.DTOs
{
    public class AddToCartRequest
    {
        public string ProductId { get; set; } = default!;
        public int Quantity { get; set; }
    }
}