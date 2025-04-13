namespace ECommerce.Application.DTOs.Response
{
    public class CookieAuthResponse : IAuthResponse
    {
        public string UserId { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
    }
}