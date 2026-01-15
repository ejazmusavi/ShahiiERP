using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ShahiiERP.Application.Abstractions;
using ShahiiERP.Application.Common.Models;
using ShahiiERP.Domain.Entities.Identity;
using ShahiiERP.Domain.Entities.Tenants;
using ShahiiERP.Infrastructure.Persistence.Contexts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ShahiiERP.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly SharedDbContext _db;
    private readonly IConfiguration _config;

    public AuthService(SharedDbContext db, IConfiguration config)
    {
        _db = db;
        _config = config;
    }

    public async Task<User?> ValidateUserAsync(string username, string password, TenantContextModel tenant)
    {
        var user = await _db.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(x => x.UserName == username && x.TenantId == tenant.TenantId);

        if (user == null)
            return null;

        if (!VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
            return null;

        return user;
    }

    public async Task<string> GenerateJwtTokenAsync(User user, TenantContextModel tenant)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim("uid", user.Id.ToString()),
            new Claim("tenant", tenant.Code),
            new Claim("role", user.Role.Name)
        };

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(12),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private bool VerifyPassword(string password, string storedHash, string storedSalt)
    {
        var saltBytes = Convert.FromBase64String(storedSalt);
        var hashBytes = KeyDerivation.Pbkdf2(
            password, saltBytes, KeyDerivationPrf.HMACSHA256, 10000, 32);

        return storedHash == Convert.ToBase64String(hashBytes);
    }
}
