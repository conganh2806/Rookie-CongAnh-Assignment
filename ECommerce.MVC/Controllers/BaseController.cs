using Microsoft.AspNetCore.Mvc;

namespace YourProjectNamespace.Controllers
{
    public class BaseController : Controller
    {
        protected readonly ILogger Logger;

        public BaseController(ILogger logger)
        {
            Logger = logger;
        }

        protected IActionResult SuccessResponse(object data, string message = "Success")
        {
            return Ok(new
            {
                success = true,
                message,
                data
            });
        }

        protected IActionResult ErrorResponse(string message = "An error occurred", 
                                                int statusCode = 500)
        {
            Logger.LogError(message);
            return StatusCode(statusCode, new
            {
                success = false,
                message
            });
        }

        protected IActionResult NotFoundResponse(string message = "Resource not found")
        {
            return NotFound(new
            {
                success = false,
                message
            });
        }
        
        protected IActionResult BadRequestResponse(string message = "Bad request")
        {
            return BadRequest(new
            {
                success = false,
                message
            });
        }
    }
}
