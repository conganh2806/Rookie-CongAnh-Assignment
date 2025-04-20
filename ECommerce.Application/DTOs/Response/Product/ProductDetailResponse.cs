namespace ECommerce.Application.DTOs
{
    public class ProductDetailResponse
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; } 
        public float Discount { get; set; } 
        public int Quantity { get; set; } 
        public int Sold { get; set; }
        public string Slug { get; set; } = default!;
        public string ImageURL { get; set; } = default!;
        public List<string> CategoryNames { get; set; } = new();
    }
}