namespace ECommerce.Application.DTOs
{
    public class ProductFeatureResponse
    {
        public string Id { get; set; } = default!;
        public string? ImageUrl { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string Slug { get; set; } = default!;
    }
}