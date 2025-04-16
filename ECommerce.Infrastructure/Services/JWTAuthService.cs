using ECommerce.Domain.Interfaces;
using ECommerce.Application.DTOs;
using ECommerce.Domain.Entities.ApplicationUser;
using Microsoft.Extensions.Options;
using ECommerce.Application.Settings;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using ECommerce.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Services.Authentication;

public class JWTAuthService : IJWTAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly JwtSettings _jwtSettings;

    public JWTAuthService(IUserRepository userRepository, 
                            IOptions<JwtSettings> jwtOptions)
    {
        _userRepository = userRepository;
        _jwtSettings = jwtOptions.Value;
    }

    public async Task<JWTAuthResponse?> RegisterAsync(RegisterRequest request)
    {
        if (await _userRepository.Entity.AnyAsync(u => u.Email == request.Email))
        {
            return null;
        }

        var user = new User
        {
            Email = request.Email,
            PasswordHash = HashPassword(request.Password),
            FirstName = request.FirstName,
            LastName = request.LastName,
            RefreshToken = Guid.NewGuid().ToString(),
            RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7),
        };

        _userRepository.Add(user);

        await _userRepository.UnitOfWork.SaveChangesAsync();

        return GenerateToken(user);
    }

    public async Task<JWTAuthResponse?> LoginAsync(LoginRequest request)
    {
        var user = await _userRepository.Entity.Where(u => u.Email == request.Email)
                                              .FirstOrDefaultAsync();

        if (user == null || !PasswordMatches(request.Password, user.PasswordHash!))
        {
            return null;
        }

        var response = GenerateToken(user);

        user.RefreshToken = response.RefreshToken;
        user.RefreshTokenExpiryTime = response.Expiration.AddDays(7);

        _userRepository.Update(user);

        await _userRepository.UnitOfWork.SaveChangesAsync();

        return response;
    }

    public async Task<JWTAuthResponse?> RefreshTokenAsync(string refreshToken)
    {

        var user = await _userRepository.Entity
                                        .FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);

        if (user == null || user.RefreshTokenExpiryTime < DateTime.UtcNow)
        {
            return null;
        }

        var accessToken = GenerateToken(user);

        var newRefreshToken = Guid.NewGuid().ToString();
        var newRefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiryTime = newRefreshTokenExpiryTime;
    
        await _userRepository.UnitOfWork.SaveChangesAsync();
        
        return new JWTAuthResponse
        {
            Token = accessToken.Token,
            RefreshToken = newRefreshToken,
            Expiration = newRefreshTokenExpiryTime
        };
    }

    private bool PasswordMatches(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }

    private string HashPassword(string password)
    {
        Console.WriteLine($"Hashing password: {BCrypt.Net.BCrypt.HashPassword(password)}");
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    private JWTAuthResponse GenerateToken(User user)
    {
        string role = "User";
        
        var key = Encoding.UTF8.GetBytes(_jwtSettings.Secret);
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Role, role)
            }),
            Expires = DateTime.UtcNow.AddHours(_jwtSettings.ExpirationHours),
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), 
                SecurityAlgorithms.HmacSha256Signature
            )
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return new JWTAuthResponse 
        {
            Token = tokenHandler.WriteToken(token),
            RefreshToken = Guid.NewGuid().ToString(),
            Expiration = tokenDescriptor.Expires!.Value
        };
    }
}
