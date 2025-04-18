using ECommerce.Domain.Entities;

namespace ECommerce.Core.Entities
{
    public class MediaFile : BaseEntity
    {
        public string FileName { get; set; } = default!;
        public string FileExtension { get; set; } = default!;
        public string ContentType { get; set; } = default!;
        public string ObjectType { get; set; } = default!;
        public string ObjectId { get; set; } = default!;
        public string? S3Key { get; set; }
        public bool IsUpload { get; set; } = false;
        public long FileSize { get; set; } 
        public string? Url { get; set; }
    }
}
