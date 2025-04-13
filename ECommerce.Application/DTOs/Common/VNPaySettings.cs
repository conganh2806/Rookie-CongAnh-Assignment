namespace ECommerce.ECommerce.Application.DTOs.Common
{
    public class VNPaySettings
    {
        public string TmnCode { get; set; } = default!;
        public string HashSecret { get; set; } = default!;
        public string VnpUrl { get; set; } = default!;
        public string ReturnUrl { get; set; } = default!;
    }
}