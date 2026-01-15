using ShahiiERP.Domain.Common;

namespace ShahiiERP.Domain.Entities.Identity;

public class Role : TenantScopedEntity
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool IsSystemRole { get; set; } = false;
}
