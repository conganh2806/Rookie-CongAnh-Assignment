namespace ECommerce.Application.DTOs.Common
{
    public class CreateMediaFileRequest
    {
        public string FileName { get; set; } = default!;
        public string FileExtension { get; set; } = default!;
        public string ContentType { get; set; } = default!;
        public string ObjectType { get; set; } = default!;
        public string ObjectId { get; set; } = default!;
        public Stream FileStream { get; set; } = Stream.Null;
    }
}