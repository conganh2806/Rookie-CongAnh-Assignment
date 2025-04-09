using ECommerce.Domain.Entities.ApplicationUser;

namespace ECommerce.Domain.Entities
{
    public class Order : BaseEntity
    {
        public string UserId { get; set; } = null!;
        public User User { get; set; }
        public int TotalAmount { get; set; }
        public string PaymentType { get; set; } 
        public string PaymentStatus { get; set; } 
        public string Status { get; set; }        
        public int Quantity { get; set; }
        public string Note { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
