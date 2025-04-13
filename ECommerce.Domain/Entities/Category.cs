namespace ECommerce.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Slug { get; set; } = null!;

        public ICollection<Product> Products { get; set; }
    }
}
