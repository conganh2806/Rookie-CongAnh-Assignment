using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.DTOs
{ 
    public class LoginRequest
    { 
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; } = default!;

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, ErrorMessage = "Password must be at least 6 characters long.", MinimumLength = 6)]
        public string Password { get; set; } = default!;

        public bool RememberMe { get; set; } = false;
    }
}