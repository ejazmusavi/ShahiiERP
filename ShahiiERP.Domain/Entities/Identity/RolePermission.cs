using ShahiiERP.Domain.Common;

namespace ShahiiERP.Domain.Entities.Identity;

public class RolePermission: TenantScopedEntity
{
    public Guid RoleId { get; set; }
    public Role Role { get; set; }

    public Guid PermissionId { get; set; }
    public Permission Permission { get; set; }
}
