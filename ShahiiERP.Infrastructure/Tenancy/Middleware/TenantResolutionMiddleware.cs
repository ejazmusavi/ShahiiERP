using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using ShahiiERP.Application.Common.Interfaces.Tenancy;

namespace ShahiiERP.Infrastructure.Tenancy.Middleware
{
    public class TenantResolutionMiddleware
    {
        private readonly RequestDelegate _next;

        public TenantResolutionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ITenantResolver resolver)
        {
            var tenant = await resolver.ResolveAsync();
            if (tenant != null)
            {
                context.Items["Tenant"] = tenant;
            }

            await _next(context);
        }
    }
}
