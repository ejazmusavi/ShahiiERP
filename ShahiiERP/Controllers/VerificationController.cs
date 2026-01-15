using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ShahiiERP.Infrastructure.Persistence.Contexts;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

[AllowAnonymous]
public class VerificationController : Controller
{
    private readonly SharedDbContext _db;
    private readonly JwtSettings _jwt;

    public VerificationController(SharedDbContext db, JwtSettings jwt)
    {
        _db = db;
        _jwt = jwt;
    }

    [HttpGet("/verification/confirm")]
    public IActionResult Confirm(string v)
    {
        if (string.IsNullOrWhiteSpace(v))
            return View("/Views/Onboarding/AlreadyVerified.cshtml");

        var handler = new JwtSecurityTokenHandler();

        try
        {
            var token = handler.ValidateToken(v, new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_jwt.Secret)),
                ValidateLifetime = true
            }, out var _);

            var jwt = token;// as JwtSecurityToken;
            var vid = Guid.Parse(jwt.Claims.First(x => x.Type == "vid").Value);
            var tid = Guid.Parse(jwt.Claims.First(x => x.Type == "tid").Value);

            var record = _db.EmailVerifications.FirstOrDefault(x => x.Id == vid);

            if (record == null)
                return View("/Views/Onboarding/AlreadyVerified.cshtml");

            if (record.IsUsed || record.IsRevoked)
                return View("/Views/Onboarding/AlreadyVerified.cshtml");

            if (record.ExpiresAt < DateTime.UtcNow)
                return View("/Views/Onboarding/AlreadyVerified.cshtml");

            // mark consumed
            record.IsUsed = true;
            record.UsedAt = DateTime.UtcNow;

            // activate user
            var user = _db.Users.First(x => x.Id == record.UserId);
            user.IsActive = true;

            // also activate tenant (optional)
            var tenant = _db.Tenants.First(x => x.Id == record.TenantId);
            tenant.IsActive = true;

            _db.SaveChanges();

            return View("/Views/Onboarding/Verified.cshtml");
        }
        catch
        {
            return View("/Views/Onboarding/AlreadyVerified.cshtml");
        }
    }
}
