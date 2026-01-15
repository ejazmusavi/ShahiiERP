using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using ShahiiERP.Application.Common.Interfaces.Persistence;
using ShahiiERP.Application.Common.Interfaces.Tenancy;
using ShahiiERP.Application.Common.Models;
using ShahiiERP.Domain.Tenants;

namespace ShahiiERP.Infrastructure.Tenancy
{
    public class TenantResolver : ITenantResolver
    {
        private readonly IHttpContextAccessor _http;
        private readonly ISharedDbContext _shared;

        public TenantResolver(IHttpContextAccessor http, ISharedDbContext shared)
        {
            _http = http;
            _shared = shared;
        }

        public async Task<TenantContextModel?> ResolveAsync()
        {
            var ctx = _http.HttpContext;
            if (ctx == null)
                return null;

            string? code = null;

            // 1. Subdomain (school.myapp.com)
            var host = ctx.Request.Host.Host;
            var parts = host.Split('.');
            if (parts.Length >= 3)
                code = parts[0].ToLower();

            // 2. URL segment (/t/{code}/dashboard)
            if (code == null)
            {
                var segments = ctx.Request.Path.Value?.Split('/', StringSplitOptions.RemoveEmptyEntries);
                if (segments?.Length > 0 && segments[0].ToLower() == "t" && segments.Length > 1)
                    code = segments[1].ToLower();
            }

            // 3. Header (X-Tenant: school)
            if (code == null && ctx.Request.Headers.TryGetValue("X-Tenant", out var tenantHeader))
                code = tenantHeader.ToString().ToLower();

            // 4. Query (?tenant=school)
            if (code == null && ctx.Request.Query.TryGetValue("tenant", out var tenantQuery))
                code = tenantQuery.ToString().ToLower();

            // 5. Cookie (tenant=school)
            if (code == null && ctx.Request.Cookies.TryGetValue("tenant", out var tenantCookie))
                code = tenantCookie.ToLower();

            // 6. Claims (after login)
            if (code == null && ctx.User.Identity?.IsAuthenticated == true)
                code = ctx.User.Claims.FirstOrDefault(c => c.Type == "tenant")?.Value?.ToLower();

            if (code == null)
                return null;

            var tenant = await _shared.Tenants.FirstOrDefaultAsync(x => x.Code.ToLower() == code);
            if (tenant == null)
                return null;

            return new TenantContextModel
            {
                TenantId = tenant.Id,
                Code = tenant.Code,
                Name = tenant.Name,
                DatabaseMode = tenant.DatabaseMode,
                ConnectionString = tenant.ConnectionString
            };
        }
    }
}
