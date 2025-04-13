namespace ECommerce.Domain.Entities
{
    public class OrderDetail : BaseEntity
    {
        public string OrderId { get; set; } = null!;
        public Order Order { get; set; } = null!;

        public string ProductId { get; set; } = null!;
        public Product Product { get; set; } = null!;

        public string ProductName { get; set; } = "";
        public int Price { get; set; }
    }
}
