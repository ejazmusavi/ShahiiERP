using ShahiiERP.Application.Common.Models;

namespace ShahiiERP.Application.Common.Interfaces.Persistence
{
    public interface ITenantDbContextFactory
    {
        ITenantDbContext Create(TenantContextModel tenant);
    }
}
