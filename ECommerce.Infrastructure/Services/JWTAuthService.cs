using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using ECommerce.Application.DTOs;
using ECommerce.Application.DTOs.Request;
using ECommerce.Application.Entities.ApplicationUser;
using ECommerce.Application.Interfaces;
using ECommerce.Application.Settings;
using ECommerce.Domain.Entities.ApplicationUser;
using ECommerce.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ECommerce.Application.Services.Authentication;

public class JWTAuthService : IJWTAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly JwtSettings _jwtSettings;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    private readonly ICurrentUser _currentUser;

    public JWTAuthService(
        IUserRepository userRepository,
        IOptions<JwtSettings> jwtOptions,
        IPasswordHasher<User> passwordHasher,
        UserManager<User> userManager,
        IMapper mapper,
        ICurrentUser currentUser
    )
    {
        _userRepository = userRepository;
        _jwtSettings = jwtOptions.Value;
        _passwordHasher = passwordHasher;
        _userManager = userManager;
        _mapper = mapper;
        _currentUser = currentUser;
    }

    public async Task<bool?> RegisterAsync(RegisterRequest request)
    {
        if (await _userRepository.Entity.AnyAsync(u => u.Email == request.Email))
        {
            return false;
        }

        var user = new User
        {
            UserName = request.Email,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            RefreshToken = Guid.NewGuid().ToString(),
            RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7),
        };

        user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);

        _userRepository.Add(user);
        await _userRepository.UnitOfWork.SaveChangesAsync();

        await _userManager.AddToRoleAsync(user, "User");

        return true;
    }

    public async Task<JWTLoginAuthResponse?> LoginAsync(LoginRequest request)
    {
        var user = await _userRepository
            .Entity.Where(u => u.Email == request.Email)
            .FirstOrDefaultAsync();

        if (user is null)
            return null;

        if (string.IsNullOrEmpty(user.PasswordHash))
            return null;

        var verifyHashedPasswordResult = _passwordHasher.VerifyHashedPassword(
            user,
            user.PasswordHash,
            request.Password
        );

        if (verifyHashedPasswordResult == PasswordVerificationResult.Failed)
        {
            return null;
        }

        if (verifyHashedPasswordResult == PasswordVerificationResult.SuccessRehashNeeded)
        {
            user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);
            _userRepository.Update(user);
            await _userRepository.UnitOfWork.SaveChangesAsync();
        }

        var response = await GenerateTokenAsync(user);

        user.RefreshToken = response.RefreshToken;
        user.RefreshTokenExpiryTime = response.Expiration.AddDays(
            _jwtSettings.RefreshTokenExpiryTime
        );

        _userRepository.Update(user);

        await _userRepository.UnitOfWork.SaveChangesAsync();

        return response;
    }

    public async Task<JWTLoginAuthResponse?> RefreshTokenAsync(RefreshTokenRequest request)
    {
        var user = await _userRepository.Entity.FirstOrDefaultAsync(u =>
            u.RefreshToken == request.Token
        );

        if (user is null || user.RefreshTokenExpiryTime < DateTime.UtcNow)
        {
            return null;
        }

        var response = await GenerateTokenAsync(user);

        user.RefreshToken = response.RefreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpiryTime);

        await _userRepository.UnitOfWork.SaveChangesAsync();

        return response;
    }

    private async Task<JWTLoginAuthResponse> GenerateTokenAsync(User user)
    {
        var roles = await _userManager.GetRolesAsync(user);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim(ClaimTypes.GivenName, user.FirstName ?? string.Empty),
            new Claim(ClaimTypes.Surname, user.LastName ?? string.Empty),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var key = Encoding.UTF8.GetBytes(_jwtSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(_jwtSettings.ExpirationHours),
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
            ),
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return new JWTLoginAuthResponse
        {
            Token = tokenHandler.WriteToken(token),
            RefreshToken = Guid.NewGuid().ToString(),
            Expiration = tokenDescriptor.Expires!.Value,
        };
    }

    public async Task<GetMeResponse?> GetMeAsync()
    {
        var user = await _userManager.FindByIdAsync(_currentUser.UserId);
        if (user is null)
            return null;

        var roles = await _userManager.GetRolesAsync(user);

        var getMeResponse = _mapper.Map<GetMeResponse>(user);
        getMeResponse.Roles = roles.ToList();

        return getMeResponse;
    }
}
