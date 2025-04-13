namespace ECommerce.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string? Slug { get; set; }

        public string? ParentId { get; set; }

        public virtual Category? Parent { get; set; }

        public virtual ICollection<Category> SubCategories { get; set; } = new HashSet<Category>();
        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
