using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShahiiERP.Application.Common.Interfaces.Persistence;
using ShahiiERP.Application.Common.Models;
using ShahiiERP.Domain.Tenants;
using ShahiiERP.Infrastructure.Persistence.Contexts;

namespace ShahiiERP.Infrastructure.Persistence.Tenancy
{
    public class TenantDbContextFactory : ITenantDbContextFactory
    {
        private readonly IConfiguration _config;

        public TenantDbContextFactory(IConfiguration config)
        {
            _config = config;
        }

        public ITenantDbContext Create(TenantContextModel tenant)
        {
            var options = new DbContextOptionsBuilder<TenantDbContext>();

            if (tenant.DatabaseMode == TenantDatabaseMode.Shared)
            {
                var sharedConn = _config.GetConnectionString("SharedTenantDb");
                options.UseSqlServer(sharedConn);
            }
            else
            {
                options.UseSqlServer(tenant.ConnectionString);
            }

            return new TenantDbContext(options.Options);
        }
    }
}
