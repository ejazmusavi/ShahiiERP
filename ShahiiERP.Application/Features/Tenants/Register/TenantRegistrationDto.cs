namespace ShahiiERP.Application.Features.Tenants.Register;

public class TenantRegistrationDto
{
    public Guid PlanId { get; set; }
    public string SchoolName { get; set; } = default!;
    public string AdminName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string? Phone { get; set; }
}
