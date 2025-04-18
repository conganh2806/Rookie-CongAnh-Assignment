using ECommerce.Application.DTOs;
using ECommerce.Application.DTOs.Common;
using ECommerce.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/media")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class MediaController : BaseController<MediaController>
    {
        private readonly IMediaFileService _mediaService;

        public MediaController(IMediaFileService mediaService, ILogger<MediaController> logger)
            : 
            base(logger)
        {
            _mediaService = mediaService;
        }

        [HttpPost("presign")]
        public async Task<IActionResult> CreatePresignedUrl(
                                        [FromBody] CreatePresignedUrlRequest request)
        {
            if (!ModelState.IsValid)
            {
                return Error("Invalid request.");
            }

            var url = await _mediaService.CreatePresignedUrlAsync(request, request.ObjectType, 
                                                                    request.ObjectId);

            return Success(url, "Presigned URL created successfully.");
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] CreateMediaFileRequest request)
        {
            var url = await _mediaService.UploadFileAsync(request);

            return Success(url, "File uploaded successfully.");
        }

        [HttpPatch("mark-uploaded/{s3Key}")]
        public async Task<IActionResult> MarkUploaded(string s3Key)
        {
            await _mediaService.UpdateStatusMediaFileAsync(s3Key);
            
            return NoContent();
        }
    }
}