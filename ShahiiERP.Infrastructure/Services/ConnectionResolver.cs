using Microsoft.Extensions.Configuration;
using ShahiiERP.Domain.Entities.Tenants;

namespace ShahiiERP.Infrastructure.Services;

public class ConnectionResolver
{
    private readonly IConfiguration _config;

    public ConnectionResolver(IConfiguration config)
    {
        _config = config;
    }

    public string ResolveSharedDbConnection()
    {
        return _config.GetConnectionString("SharedDB");
    }

    public string ResolveTenantDbConnection(Tenant tenant)
    {
        return tenant.ConnectionString;
    }
}
