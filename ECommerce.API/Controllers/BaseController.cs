using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using ECommerce.Application.Common;

namespace ECommerce.API.Controllers
{
    [ApiController]
    public abstract class BaseController<T> : ControllerBase where T : class
    {
        protected readonly ILogger<T> _logger;

        protected BaseController(ILogger<T> logger)
        {
            _logger = logger;
        }

        protected IActionResult Success<TData>(TData data, string message = "Success")
        {
            var response = BaseResponse<TData>.Success(data, message);
            return StatusCode((int)response.StatusCode, response);
        }

        protected IActionResult Error(string message,
                                        HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            var response = BaseResponse<object>.Fail(message, statusCode);
            _logger.LogWarning("Error response: {Message}", message);
            return StatusCode((int)statusCode, response);
        }

        protected Guid? CurrentUserId =>
            User.Identity?.IsAuthenticated == true
                ? Guid.TryParse(User.FindFirst("sub")?.Value, out var id) ? id : null
                : null;
    }
}
