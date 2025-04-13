using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.DTOs
{ 
    public class RegisterRequest
    { 
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; } = default!;

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, ErrorMessage = "Password must be at least 6 characters long.",
                            MinimumLength = 6)]
        public string Password { get; set; } = default!;

        public string FirstName { get; set; } = default!;

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
        public string LastName { get; set; } = default!;
    }
}