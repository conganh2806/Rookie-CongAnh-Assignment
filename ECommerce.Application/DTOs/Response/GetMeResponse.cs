namespace ECommerce.Application.Entities.ApplicationUser
{
    public class GetMeResponse
    {
        public string Id { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FullName => $"{FirstName} {LastName}";
        public IList<string> Roles { get; set; } = new List<string>();
        public DateTimeOffset? DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public string? Country { get; set; }
    }
}
