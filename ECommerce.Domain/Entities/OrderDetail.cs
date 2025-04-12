namespace ECommerce.Domain.Entities
{
    public class OrderDetail : BaseEntity
    {
        public string OrderId { get; set; } = default!;
        public string ProductId { get; set; } = default!;
        public string ProductName { get; set; } = default!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public virtual Order Order { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
