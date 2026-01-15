using ShahiiERP.Application.Common.Models;
using ShahiiERP.Domain.Entities.Identity;
using ShahiiERP.Domain.Entities.Tenants;

namespace ShahiiERP.Application.Abstractions;

public interface IAuthService
{
    Task<User?> ValidateUserAsync(string username, string password, TenantContextModel tenant);
    Task<string> GenerateJwtTokenAsync(User user, TenantContextModel tenant);
}
