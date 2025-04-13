namespace ECommerce.Application.DTOs
{
    public class ProductDTO
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; } 
        public float Discount { get; set; } 
        public int Quantity { get; set; } 
        public int Sold { get; set; } 
        public string CategoryId { get; set; } = default!;
        public string Slug { get; set; } = default!;
        public string ImageURL { get; set; } = default!;
        public string CategoryName { get; set; } = default!;
    }
}
