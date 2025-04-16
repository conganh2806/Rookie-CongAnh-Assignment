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

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);
            
            builder.Property(p => p.Slug)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasIndex(p => p.Slug).IsUnique();

            builder.Property(p => p.Description)
                .HasMaxLength(1000);

            builder.Property(p => p.Price)
                .IsRequired();

            builder.Property(p => p.Discount);

            builder.Property(p => p.Quantity)
                .IsRequired();
            
            builder.Property(p => p.Sold)
                   .IsRequired();

            builder.Property(p => p.ProductStatus)
                   .IsRequired();
            
            builder.Property(p => p.ImageURL)
                .HasMaxLength(500);

            builder.Property(p => p.IsFeatured)
                .HasDefaultValue(false);

            builder.HasMany(c => c.Categories)
                .WithMany(p => p.Products);
        }
    }
}