using Microsoft.AspNetCore.Http;
using ShahiiERP.Application.Abstractions;
using ShahiiERP.Application.Common.Interfaces.Tenancy;

public class TenantResolutionMiddleware : IMiddleware
{
    private readonly ITenantResolver _tenantResolver;

    public TenantResolutionMiddleware(ITenantResolver tenantResolver)
    {
        _tenantResolver = tenantResolver;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var host = context.Request.Host.Host;
        var segments = context.Request.Path.Value?.Split('/', StringSplitOptions.RemoveEmptyEntries);

        string? subdomain = null;
        string? pathSegment = null;
        string? tenantCode = null;

        // X → subdomain
        var parts = host.Split('.');
        if (parts.Length > 2)
            subdomain = parts[0];

        // Y → first path segment
        if (segments?.Length > 0)
            pathSegment = segments[0];

        // Z will be handled in login when user submits tenant code

        context.Items["subdomain"] = subdomain;
        context.Items["pathSegment"] = pathSegment;

        await next(context);
    }
}
