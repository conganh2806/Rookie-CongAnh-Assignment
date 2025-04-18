using ECommerce.Application.Common.Utilities.Exceptions;
using ECommerce.Application.Domain.Interfaces;
using ECommerce.Application.DTOs.Common;
using ECommerce.Application.Interfaces;
using ECommerce.Core.Entities;
using ECommerce.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;

namespace ECommerce.Infrastructure.Services
{
    public class MediaFileService : IMediaFileService
    {
        private readonly IMinioClient _minioClient;
        private readonly MinioSettings _minio;
        private readonly IMediaFileRepository _mediaFileRepository;
        private readonly IProductRepository _productRepository;

        public MediaFileService(
            IOptions<MinioSettings> options,
            IMinioClient minioClient,
            IMediaFileRepository mediaFileRepository,
            IProductRepository productRepository)
        {
            _minio = options.Value;
            _minioClient = minioClient;
            _mediaFileRepository = mediaFileRepository;
            _productRepository = productRepository;
        }

        public async Task<string> CreatePresignedUrlAsync(CreatePresignedUrlRequest request, 
                                                            string objectType, string objectId)
        {
            var fileExtension = request.ContentType.Split("/")[1];

            // create s3 Key
            var s3Key = $"{objectType}/{Guid.NewGuid()}.{fileExtension}";

            // check exist record
            await CheckExistMediaFileRecord(objectType, objectId, s3Key);

            // Check exist bucket
            var bucketExists = await _minioClient.BucketExistsAsync(
                new BucketExistsArgs().WithBucket(_minio.BucketName)
            );

            if (!bucketExists)
            {
                await _minioClient.MakeBucketAsync(
                    new MakeBucketArgs().WithBucket(_minio.BucketName)
                );
            }

            // create mediaFile
            var mediaFile = new MediaFile()
            {
                FileName = request.FileName,
                FileExtension = fileExtension,
                ContentType = request.ContentType,
                ObjectType = objectType,
                ObjectId = objectId,
                S3Key = s3Key,
                IsUpload = false
            };

            _mediaFileRepository.Add(mediaFile);
            await _mediaFileRepository.UnitOfWork.SaveChangesAsync();

            // Create PresignUrl
            var presignedUrl = await _minioClient.PresignedPutObjectAsync(
                new PresignedPutObjectArgs().WithBucket(_minio.BucketName)
                                            .WithObject(s3Key)
                                            .WithExpiry(_minio.ExpiredHoursPresignUrl * 3600));

            return presignedUrl;
        }

        public async Task<string> UploadFileAsync(CreateMediaFileRequest request)
        {
            var s3Key = $"{request.ObjectType}/{Guid.NewGuid()}.{request.FileExtension.TrimStart('.')}";

            var bucketExists = await _minioClient.BucketExistsAsync(
                new BucketExistsArgs().WithBucket(_minio.BucketName)
            );

            if (!bucketExists)
            {
                await _minioClient.MakeBucketAsync(
                    new MakeBucketArgs().WithBucket(_minio.BucketName));
            }

            await CheckExistMediaFileRecord(request.ObjectType, request.ObjectId, s3Key);

            var mediaFile = new MediaFile()
            {
                FileName = request.FileName,
                FileExtension = request.FileExtension,
                ContentType = request.ContentType,
                ObjectType = request.ObjectType,
                ObjectId = request.ObjectId,
                S3Key = s3Key,
                IsUpload = true
            };

            _mediaFileRepository.Add(mediaFile);


            var resp = await _minioClient.PutObjectAsync(new PutObjectArgs()
                .WithBucket(_minio.BucketName)
                .WithObject(s3Key)
                .WithStreamData(request.FileStream)
                .WithObjectSize(request.FileStream.Length)
                .WithContentType(request.ContentType)
            );

            await UpdateImageUrl(objectType: request.ObjectType, 
                                    objectId: request.ObjectId, s3Key);

            await _mediaFileRepository.UnitOfWork.SaveChangesAsync();

            return $"http://{_minio.Endpoint}/{_minio.BucketName}/{s3Key}";
        }

        public async Task UpdateStatusMediaFileAsync(string s3Key)
        {
            var mediaFile = await _mediaFileRepository.Entity.FirstOrDefaultAsync(
                                                                s => s.S3Key == s3Key);

            if (mediaFile == null)
            {
                throw new NotFoundException($"Not found mediaFile s3Key={s3Key}");
            }

            mediaFile.IsUpload = true;
            _mediaFileRepository.Update(mediaFile);

            if (mediaFile.S3Key is not null)
            {
                await UpdateImageUrl(mediaFile.ObjectType, mediaFile.Id, mediaFile.S3Key);
            }

            await _mediaFileRepository.UnitOfWork.SaveChangesAsync();
        }

        private async Task CheckExistMediaFileRecord(string objectType, string objectId, 
                                                    string s3Key)
        {
            var existedRecord = await _mediaFileRepository.Entity
            .FirstOrDefaultAsync(_ => _.ObjectId == objectId && _.ObjectType == objectType);

            if (existedRecord is not null)
            {
                _mediaFileRepository.Delete(existedRecord);
                await _minioClient.RemoveObjectAsync(new RemoveObjectArgs()
                    .WithBucket(_minio.BucketName)
                    .WithObject(s3Key));
            }
        }

        private async Task UpdateImageUrl(string objectType, string objectId, string imageUrl)
        {
            switch (objectType)
            {
                case "product":
                    var product = await _productRepository.Entity.FirstOrDefaultAsync(
                        p => p.Id == objectId
                        );

                    if (product is null)
                    {
                        throw new NotFoundException($"Not Found Product Id={objectId}");
                    }

                    product.ImageURL = imageUrl;

                    break;
                default:
                    throw new NotSupportedException(
                        $"ObjectType '{objectType}' is not supported."
                    );
            }

        }
    }
}