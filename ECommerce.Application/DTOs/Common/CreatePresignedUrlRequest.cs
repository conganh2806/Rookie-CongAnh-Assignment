namespace ECommerce.Application.DTOs.Common
{
    public class CreatePresignedUrlRequest
    {
        public string FileName { get; set; } = default!;
        public string ContentType { get; set; } = default!;
        public string ObjectType { get; set; } = default!;
        public string ObjectId { get; set; } = default!;
    }
}