using Microsoft.AspNetCore.Http;

namespace ECommerce.Application.DTOs.Request
{
    public class UploadMediaFileRequest
    { 
        public IFormFile File { get; set; } = default!;
        public string ProductId { get; set; } = default!;
    }
}