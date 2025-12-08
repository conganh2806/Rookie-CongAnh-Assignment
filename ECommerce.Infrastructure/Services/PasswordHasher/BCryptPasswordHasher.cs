using ECommerce.Domain.Entities.ApplicationUser;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Infrastructure.Services.PasswordHasher
{
    public class BCryptPasswordHasher : IPasswordHasher<User>
    {
        public string HashPassword(User user, string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public PasswordVerificationResult VerifyHashedPassword(
            User user,
            string hashedPassword,
            string providedPassword
        )
        {
            if (BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword))
            {
                if (BCrypt.Net.BCrypt.PasswordNeedsRehash(hashedPassword, newMinimumWorkLoad: 12))
                {
                    return PasswordVerificationResult.SuccessRehashNeeded;
                }
                return PasswordVerificationResult.Success;
            }
            return PasswordVerificationResult.Failed;
        }
    }
}
