using System.Net;

namespace ECommerce.Application.Common 
{ 
    public class BaseResponse<T> 
    { 
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; } = default!;
        public T? Data { get; set; }

        public BaseResponse() {}

        public BaseResponse(HttpStatusCode statusCode, string message, T? data = default)
        {
            StatusCode = statusCode;
            Message = message;
            Data = data;
        }

        public static BaseResponse<T> Success(T data, string message) =>
            new()
            {
                StatusCode = HttpStatusCode.OK,
                Message = message,
                Data = data
            };

        public static BaseResponse<T> Fail(string message, HttpStatusCode statusCode) =>
            new()
            {
                StatusCode = statusCode,
                Message = message,
                Data = default
            };
    }
}