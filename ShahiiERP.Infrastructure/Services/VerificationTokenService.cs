using Microsoft.IdentityModel.Tokens;
using ShahiiERP.Infrastructure.Persistence.Contexts;
using ShahiiERP.Shared.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

public class VerificationTokenService
{
    private readonly SharedDbContext _db;
    private readonly JwtSettings _settings;

    public VerificationTokenService(SharedDbContext db, JwtSettings settings)
    {
        _db = db;
        _settings = settings;
    }

    public async Task<string> GenerateAsync(Guid userId, Guid tenantId)
    {
        var verificationId = Guid.NewGuid();
        var expiresAt = DateTime.UtcNow.AddHours(24);

        var hash = ComputeHash(verificationId.ToString());

        _db.EmailVerifications.Add(new EmailVerification
        {
            Id = verificationId,
            UserId = userId,
            TenantId = tenantId,
            TokenHash = hash,
            ExpiresAt = expiresAt,
            CreatedAt = DateTime.UtcNow
        });

        await _db.SaveChangesAsync();

        var handler = new JwtSecurityTokenHandler();

        var token = handler.CreateToken(new SecurityTokenDescriptor
        {
            Expires = expiresAt,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Secret)),
                SecurityAlgorithms.HmacSha256),
            Claims = new Dictionary<string, object>
            {
                ["vid"] = verificationId,
                ["tid"] = tenantId
            }
        });

        return handler.WriteToken(token);
    }

    private static string ComputeHash(string input)
    {
        using var sha = SHA256.Create();
        var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
        return Convert.ToBase64String(bytes);
    }
}
