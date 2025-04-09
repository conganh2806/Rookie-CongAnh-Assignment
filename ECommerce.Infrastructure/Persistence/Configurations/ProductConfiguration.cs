using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Persistence.Configurations
{
    public class ProductConfiguration : EntityConfiguration<Product> 
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            base.Configure(builder);

            builder.ToTable("product");

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);
            
            builder.Property(p => p.Description)
                .HasMaxLength(1000);
            
            builder.Property(p => p.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
            
            builder.Property(p => p.ImageURL)
                .HasMaxLength(500);
            
            builder.Property(p => p.CategoryId)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}