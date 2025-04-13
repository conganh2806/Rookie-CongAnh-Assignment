using Microsoft.AspNetCore.Identity;


namespace ECommerce.Domain.Entities.ApplicationUser
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? RefreshToken { get; set; } = default!;
        public DateTimeOffset? RefreshTokenExpiryTime { get; set; }        
        public DateTimeOffset? DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public string? Country { get; set; }
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}