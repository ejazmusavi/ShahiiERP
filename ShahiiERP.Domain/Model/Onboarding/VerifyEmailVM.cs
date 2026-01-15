namespace ShahiiERP.Domain.Models.Onboarding;

public class VerifyEmailVM
{
    public string TenantCode { get; set; } = default!;
    public string Token { get; set; } = default!;
}
