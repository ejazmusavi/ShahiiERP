using ShahiiERP.Application.Common;
using ShahiiERP.Application.Features.Tenants.Register;

namespace ShahiiERP.Application.Abstractions;

public interface IRegisterTenantService
{
    Task<OperationResult> RegisterAsync(TenantRegistrationDto cmd);
}
