namespace ECommerce.Application.DTOs
{
    public class CartDto
    {
        public string UserId { get; set; } = default!;
        public List<CartItemDto> Items { get; set; } = new();
    }
}
