using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace ShahiiERP.Infrastructure.Security;

public static class PasswordHasher
{
    public static (string hash, string salt) HashPassword(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(16);
        var hash = KeyDerivation.Pbkdf2(
            password, salt,
            KeyDerivationPrf.HMACSHA256,
            10000, 32);

        return (Convert.ToBase64String(hash), Convert.ToBase64String(salt));
    }
}
