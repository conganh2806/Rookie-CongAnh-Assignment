namespace ECommerce.Application.DTOs
{
    public class CategoryDto
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Slug { get; set; } = default!;
        
        public string? ParentId { get; set; }

        public List<CategoryDto> SubCategories { get; set; } = new();
    }
}