using ECommerce.Domain.Entities.Enum;

namespace ECommerce.Domain.Entities
{
    public class Product : BaseEntity
    { 
        public string Slug { get; set; } = default!; 
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public float Discount { get; set; }
        public int Quantity { get; set; }
        public int Sold { get; set; }
        public ProductStatus ProductStatus { get; set; }
        public string? ImageURL { get; set; }
        public bool IsFeatured { get; set; }
        
        public virtual ICollection<Category> Categories { get; set; } = new HashSet<Category>();
        public virtual OrderDetail OrderDetail { get; set; } = default!;
    }
}