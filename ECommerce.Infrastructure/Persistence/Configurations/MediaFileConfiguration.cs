using ECommerce.Core.Entities;
using ECommerce.Infrastructure.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Persistence.Configurations
{
    public class MediaFileConfiguration : EntityConfiguration<MediaFile> 
    {
        public override void Configure(EntityTypeBuilder<MediaFile> builder)
        {
            base.Configure(builder);

            builder.Property(m => m.FileName)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(m => m.FileExtension)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(m => m.ContentType)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(m => m.ObjectType)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(m => m.ObjectId)
                .IsRequired();

            builder.Property(m => m.S3Key)
                .HasMaxLength(512);

            builder.Property(m => m.IsUpload)
                .IsRequired();

            builder.Property(m => m.FileSize);

            builder.Property(m => m.Url);
        }
    }
}