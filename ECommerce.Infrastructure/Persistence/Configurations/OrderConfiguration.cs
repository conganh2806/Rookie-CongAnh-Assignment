using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Persistence.Configurations
{
    public class OrderConfiguration : EntityConfiguration<Order>
    {
        public override void Configure(EntityTypeBuilder<Order> builder)
        {
            base.Configure(builder);

            builder.Property(o => o.UserId).IsRequired();

            builder.Property(o => o.TotalAmount)
                    .IsRequired();

            builder.Property(o => o.PaymentType);

            builder.Property(o => o.PaymentStatus);
                    
            builder.Property(o => o.Status)
                    .IsRequired();
                    
            builder.Property(o => o.Note).HasMaxLength(500);
            
            builder.HasMany(o => o.OrderDetails)
                .WithOne(od => od.Order)
                .HasForeignKey(od => od.OrderId);
        }
    }
}