namespace ECommerce.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Slug { get; set; } = string.Empty;

        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
