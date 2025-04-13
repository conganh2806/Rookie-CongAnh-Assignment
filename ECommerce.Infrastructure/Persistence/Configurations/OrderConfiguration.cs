using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Persistence.Configurations
{
    public class OrderConfiguration : EntityConfiguration<Order>
    {
        public override void Configure(EntityTypeBuilder<Order> builder)
        {
            base.Configure(builder);

            builder.HasKey(o => o.Id);
            builder.Property(o => o.UserId).IsRequired();
            builder.Property(o => o.TotalAmount).IsRequired();
            builder.Property(o => o.PaymentType).IsRequired();
            builder.Property(o => o.PaymentStatus).IsRequired();
            builder.Property(o => o.Status).IsRequired();
            builder.Property(o => o.Quantity).IsRequired();
            builder.Property(o => o.Note).HasMaxLength(500);
            builder.Property(o => o.CreatedAt);

            builder.HasMany(o => o.OrderDetails)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId);
        }
    }
}