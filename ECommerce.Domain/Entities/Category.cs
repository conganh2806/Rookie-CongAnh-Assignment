namespace ECommerce.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string? Slug { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
