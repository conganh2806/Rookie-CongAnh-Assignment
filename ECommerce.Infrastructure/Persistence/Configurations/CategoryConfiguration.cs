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

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).IsRequired();
            builder.Property(c => c.Slug).IsRequired();
            builder.Property(c => c.CreatedAt);
            builder.Property(c => c.UpdatedAt);
            builder.Property(c => c.IsDeleted);

            builder.HasMany(c => c.Products)
                .WithMany(p => p.Categories);
        }
    }
}