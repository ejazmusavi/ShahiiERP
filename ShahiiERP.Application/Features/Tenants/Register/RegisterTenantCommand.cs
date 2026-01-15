using MediatR;

namespace ShahiiERP.Application.Features.Tenants.Register;

public record RegisterTenantCommand(
    string SchoolName,
    string AdminName,
    string AdminEmail,
    string AdminPassword
) : IRequest<TenantRegistrationResult>;
