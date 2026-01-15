using ShahiiERP.Domain.Common;
using ShahiiERP.Domain.Entities.Tenants;
using System.Data;

namespace ShahiiERP.Domain.Entities.Identity;

public class User : TenantScopedEntity
{

    public Guid? CampusId { get; set; }
    public Campus? Campus { get; set; }

    public string UserName { get; set; }
    public string Email { get; set; }
    public string? PhoneNumber { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }

    public string PasswordHash { get; set; }
    public string PasswordSalt { get; set; }

    public bool IsActive { get; set; } = true;
    public DateTime? LastLoginAt { get; set; }

    public Guid RoleId { get; set; }
    public Role Role { get; set; }
}
