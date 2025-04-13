using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Persistence.Configurations;

public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
{
    public void Configure(EntityTypeBuilder<OrderDetail> builder)
    {
        builder.Property(od => od.OrderId)
                .IsRequired();
                
        builder.Property(od => od.ProductId)
                .IsRequired();

        builder.Property(od => od.ProductName);

        builder.Property(od => od.Price);

        builder.HasOne(od => od.Order)
               .WithMany(o => o.OrderDetails)
               .HasForeignKey(od => od.OrderId);

        builder.HasOne(od => od.Product)
               .WithOne(p => p.OrderDetail);
    }
}
