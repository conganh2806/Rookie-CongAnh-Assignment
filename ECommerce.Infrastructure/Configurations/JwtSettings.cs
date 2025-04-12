namespace ECommerce.Application.Settings;

public class JwtSettings
{
    public string Issuer { get; set; } = default!;
    public string Audience { get; set; } = default!;
    public string Secret { get; set; } = default!;
    public int ExpirationHours { get; set; } = 1;
}