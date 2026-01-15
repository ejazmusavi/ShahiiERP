using ShahiiERP.Application.Common.Models;
using ShahiiERP.Domain.Entities.Tenants;

namespace ShahiiERP.Application.Common.Interfaces.Tenancy
{
    public interface ITenantProvisioner
    {
        Task ProvisionAsync(TenantContextModel tenant);
    }
}
