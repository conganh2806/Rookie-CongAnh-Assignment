namespace ECommerce.Application.DTOs.Common
{
    public class CreatePresignedUrlRequest
    {
        public string ContentType { get; set; } = default!;
        public string FileName { get; set; } = default!;
        public int FileSize { get; set; }
    }
}