using MediatR;
using ShahiiERP.Application.Abstractions;

namespace ShahiiERP.Application.Features.Tenants.Register;

public class RegisterTenantHandler : IRequestHandler<RegisterTenantCommand, TenantRegistrationResult>
{
    private readonly ITenantProvisioningService _provisioning;

    public RegisterTenantHandler(ITenantProvisioningService provisioning)
    {
        _provisioning = provisioning;
    }

    public async Task<TenantRegistrationResult> Handle(RegisterTenantCommand request, CancellationToken cancellationToken)
    {
        var dto = new TenantRegistrationDto
        {
            SchoolName = request.SchoolName,
            AdminName = request.AdminName,
            Email = request.AdminEmail,
            Password = request.AdminPassword
        };

        return await _provisioning.ProvisionAsync(dto);
    }
}
