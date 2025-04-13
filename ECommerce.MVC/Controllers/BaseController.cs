using Microsoft.AspNetCore.Mvc;

namespace ECommerce.MVC.Controllers
{
    public abstract class BaseController<T> : Controller where T : class
    {
        protected readonly ILogger<T> _logger;

        protected BaseController(ILogger<T> logger)
        {
            _logger = logger;
        }

        protected Guid? CurrentUserId =>
            User.Identity?.IsAuthenticated == true
                ? Guid.TryParse(User.FindFirst("sub")?.Value, out var id) ? id : null
                : null;

        protected void SetSuccessMessage(string message)
        {
            TempData["SuccessMessage"] = message;
        }

        protected void SetErrorMessage(string message)
        {
            TempData["ErrorMessage"] = message;
        }

        protected void SetWarningMessage(string message)
        {
            TempData["WarningMessage"] = message;
        }

        protected void SetInfoMessage(string message)
        {
            TempData["InfoMessage"] = message;
        }

        protected IActionResult RedirectWithSuccess(string url, string message)
        {
            SetSuccessMessage(message);
            return Redirect(url);
        }

        protected IActionResult RedirectWithError(string url, string message)
        {
            SetErrorMessage(message);
            return Redirect(url);
        }
    }
}
