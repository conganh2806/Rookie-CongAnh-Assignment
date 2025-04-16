namespace ECommerce.Application.DTOs.Common
{
    public class CreateMediaFileRequest
    {
        public Stream FileStream { get; set; } = default!;
        public string FileName { get; set; } = default!;
        public string FileExtension { get; set; } = default!;
        public string ContentType { get; set; } = default!;
        public string ObjectType { get; set; } = default!;
        public string ObjectId { get; set; } = default!;
    }
}