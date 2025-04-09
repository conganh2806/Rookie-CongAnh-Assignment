using ECommerce.Domain.Entities.User;

namespace ECommerce.Domain.Entities
{
    public class Order : BaseEntity
    {
        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; }
        public int TotalAmount { get; set; }
        public string PaymentType { get; set; } 
        public string PaymentStatus { get; set; } 
        public string Status { get; set; }        
        public int Quantity { get; set; }
        public string Note { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
