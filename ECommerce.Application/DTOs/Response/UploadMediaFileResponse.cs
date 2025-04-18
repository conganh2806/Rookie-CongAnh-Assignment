namespace ECommerce.Application.DTOs
{
    public class UploadMediaFileResponse
    {
        public string FileName { get; set; } = default!;
        public string FileExtension { get; set; } = default!;
        public string ContentType { get; set; } = default!;
        public string FileUrl { get; set; } = default!;
    }
}