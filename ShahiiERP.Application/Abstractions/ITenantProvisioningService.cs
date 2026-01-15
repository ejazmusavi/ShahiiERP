using ShahiiERP.Application.Features.Tenants.Register;

namespace ShahiiERP.Application.Abstractions;

public interface ITenantProvisioningService
{
    Task<TenantRegistrationResult> ProvisionAsync(TenantRegistrationDto dto);
}
