using ECommerce.Domain.Entities.ApplicationUser;
using ECommerce.Domain.Enum;

namespace ECommerce.Domain.Entities
{
    public class Order : BaseEntity
    {
        public string UserId { get; set; } = default!;
        public decimal TotalAmount { get; set; }
        public PaymentType PaymentType { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public OrderStatus Status { get; set; }
        public string? Note { get; set; }
        
        public virtual User User { get; set; } = default!; 
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new HashSet<OrderDetail>();
    }
}
