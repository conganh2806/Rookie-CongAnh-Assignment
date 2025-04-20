namespace ECommerce.MVC.Models;

public class ErrorViewModel
{
    public string Message { get;  set; } = default!;
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
