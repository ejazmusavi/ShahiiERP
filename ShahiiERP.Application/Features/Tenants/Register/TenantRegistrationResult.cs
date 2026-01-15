namespace ShahiiERP.Application.Features.Tenants.Register;

public class TenantRegistrationResult
{
    public bool Success { get; set; }
    public Guid TenantId { get; set; }
    public string TenantCode { get; set; } = default!;
    public string LoginUrl { get; set; } = default!;
    public List<string>? Errors { get; set; }
}
