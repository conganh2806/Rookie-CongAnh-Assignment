using Microsoft.AspNetCore.Identity;


namespace ECommerce.Domain.Entities.ApplicationUser
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string RefreshToken { get; set; } = default!;
        public DateTimeOffset RefreshTokenExpiryTime { get; set; }        
        public DateTimeOffset DateOfBirth { get; set; }
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}