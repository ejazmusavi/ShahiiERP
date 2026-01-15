using Microsoft.AspNetCore.Builder;

namespace ShahiiERP.Infrastructure.Tenancy.Middleware
{
    public static class TenantResolutionExtensions
    {
        public static IApplicationBuilder UseTenantResolution(this IApplicationBuilder app)
        {
            return app.UseMiddleware<TenantResolutionMiddleware>();
        }
    }
}
