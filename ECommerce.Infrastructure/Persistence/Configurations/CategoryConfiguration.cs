using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Persistence.Configurations
{
    public class CategoryConfiguration : EntityConfiguration<Category> 
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            base.Configure(builder);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Slug);

            builder.HasIndex(c => c.Slug).IsUnique();

            builder.HasMany(c => c.Products)
                .WithMany(p => p.Categories);
        }
    }
}