using ECommerce.Application.DTOs.Common;

namespace ECommerce.Application.Interfaces
{
    public interface IMediaFileService
    {
        Task<string> UploadFileAsync(CreateMediaFileRequest fileStream);
        Task<string> CreatePresignedUrlAsync(CreatePresignedUrlRequest request, string objectType, string objectId);
        Task UpdateStatusMediaFileAsync(string s3Key);
    }
}
