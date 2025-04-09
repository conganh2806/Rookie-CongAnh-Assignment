namespace ECommerce.Domain.Entities
{
    public class Product : BaseEntity
    { 
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ImageURL { get; set; } = string.Empty;
        public string CategoryId { get; set; }
        public ICollection<Category> Categories { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } 
    }
}