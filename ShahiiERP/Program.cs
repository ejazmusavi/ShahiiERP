using Microsoft.AspNetCore.Identity;
using ShahiiERP.Application;
using ShahiiERP.Domain.Entities.Identity;
using ShahiiERP.Infrastructure;
using ShahiiERP.Infrastructure.Persistence.Contexts;
using ShahiiERP.Infrastructure.Seed;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
// Application & Infrastructure setup
builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructure(builder.Configuration);

// Authentication
builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

// Tenant resolution middleware
builder.Services.AddScoped<TenantResolutionMiddleware>();


builder.Services.AddControllersWithViews();
var app = builder.Build();
// Enable tenant resolution BEFORE MVC executes
app.UseMiddleware<TenantResolutionMiddleware>();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var sharedDb = services.GetRequiredService<SharedDbContext>();
    await DatabaseSeeder.SeedAsync(sharedDb);
    await SharedBillingSeeder.SeedAsync(sharedDb);
    //var tenantManager = services.GetRequiredService<ITenantManager>();
    //var tenants = await tenantManager.GetAllTenantsAsync();

    //foreach (var tenant in tenants)
    //{
    //    var tenantDb = tenantManager.CreateTenantDbContext(tenant);
    //    await TenantDbSeeder.SeedAsync(tenantDb);
    //}
}
app.Run();

