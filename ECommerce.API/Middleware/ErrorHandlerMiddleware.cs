using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using ECommerce.Application.Common.Utilities.Exceptions;
using ECommerce.Common.Utilities;
using ECommerce.JsonNamingPolicy;

namespace ECommerce.API.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {

            var response = context.Response;

            if (response.HasStarted)
                return;
            response.ContentType = "application/json";

            var errorDetail = new ErrorDetails()
            {
                StatusCode = (int)System.Net.HttpStatusCode.InternalServerError,
                Message = exception.Message
            };
            switch (exception)
            {
                case NotFoundException e:
                    // Custom not found
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                case UnAuthorizedException e:
                    // Custom not found
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    break;
                case ForbiddenException e:
                    response.StatusCode = (int)HttpStatusCode.Forbidden;
                    break;
                case BadRequestException e:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case ValidationException e:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorDetail.Message = "Object validation error.";
                    break;
                case Minio.Exceptions.AccessDeniedException e:
                    response.StatusCode = (int)HttpStatusCode.Forbidden;
                    errorDetail.Message = "Access denied while uploading to MinIO.";
                    break;

                case Minio.Exceptions.AuthorizationException e:
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    errorDetail.Message = @"MinIO authorization failed. 
                                            Please check AccessKey/SecretKey.";
                    break;

                case Minio.Exceptions.BucketNotFoundException e:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    errorDetail.Message = "The specified MinIO bucket does not exist.";
                    break;

                case Minio.Exceptions.InvalidBucketNameException e:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorDetail.Message = "Invalid MinIO bucket name.";
                    break;

                case Minio.Exceptions.InvalidObjectNameException e:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorDetail.Message = "Invalid object name for MinIO.";
                    break;
                default:
                    // unhandled error
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorDetail.Message = exception.Message;
                    break;
            }

            errorDetail.StatusCode = response.StatusCode;
            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = SnakeCaseNamingPolicy.Instance,
                WriteIndented = true
            };
            await response.WriteAsync(JsonSerializer.Serialize(errorDetail, serializeOptions));
        }
    }
}