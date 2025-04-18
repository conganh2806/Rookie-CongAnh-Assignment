namespace ECommerce.Application.DTOs.Common
{
    public class CreatePresignedUrlRequest
    {
        public string FileName { get; set; } = default!;
        public string ContentType { get; set; } = default!;
        public string ObjectType { get; set; } = default!; // e.g. "product"
        public string ObjectId { get; set; } = default!;   // e.g. product Id
    }
}