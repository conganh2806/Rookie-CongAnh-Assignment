namespace ECommerce.MVC.Models
{
    public class ProfileViewModel
    {
        public Guid UserId { get; set; }

        public string Email { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
