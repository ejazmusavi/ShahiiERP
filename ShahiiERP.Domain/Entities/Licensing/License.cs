using ShahiiERP.Domain.Common;

namespace ShahiiERP.Domain.Entities.Licensing;

public class License : TenantScopedEntity
{
    public string LicenseKey { get; set; }
    public int StudentsLimit { get; set; }
    public string Modules { get; set; } // JSON array
    public DateTime ExpiresAt { get; set; }
    public string Signature { get; set; }
}
