using ShahiiERP.Domain.Common;

namespace ShahiiERP.Domain.Entities.Tenants;

public class Campus : TenantScopedEntity
{

    public string Name { get; set; }
    public string Code { get; set; }

    public string? Address { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }

    public string? Phone { get; set; }
    public string? Email { get; set; }

    public bool IsActive { get; set; } = true;
}
