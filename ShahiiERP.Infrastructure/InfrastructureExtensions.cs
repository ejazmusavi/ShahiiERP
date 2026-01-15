using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShahiiERP.Application.Abstractions;
using ShahiiERP.Application.Common.Interfaces.Persistence;
using ShahiiERP.Application.Common.Interfaces.Tenancy;
using ShahiiERP.Application.Features.Tenants.Register;
using ShahiiERP.Infrastructure.Persistence.Contexts;
using ShahiiERP.Infrastructure.Persistence.Tenancy;
using ShahiiERP.Infrastructure.Services;
using ShahiiERP.Infrastructure.Tenancy;

namespace ShahiiERP.Infrastructure
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDbContext<SharedDbContext>(options =>
            //    options.UseSqlServer(configuration.GetConnectionString("SharedDB"))
            //);

            services.AddDbContext<SharedDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("SharedDB")));

            services.AddScoped<ISharedDbContext>(provider => provider.GetRequiredService<SharedDbContext>());

            // Tenancy
            services.AddScoped<ITenantDbContextFactory, TenantDbContextFactory>();
            services.AddScoped<ITenantResolver, TenantResolver>();
            //services.AddScoped<ITenantProvisioner, TenantProvisioner>();
            services.AddScoped<ITenantProvisioningService, TenantProvisioningService>();


            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ISubscriptionPlanQueryService, SubscriptionPlanQueryService>();
            services.AddScoped<IRegisterTenantService, RegisterTenantService>();
            // TODO: add repository registrations later
            // services.AddScoped<IStudentRepository, StudentRepository>();

            var emailProvider = configuration["Email:Provider"]?.ToUpper();

            switch (emailProvider)
            {
                case "SMTP":
                    services.AddSingleton<IEmailProvider>(sp =>
                    {
                        var s = configuration.GetSection("Email:Smtp").Get<SmtpSettings>();
                        return new SmtpEmailProvider(s);
                    });
                    break;

                case "CONSOLE":
                    services.AddSingleton<IEmailProvider, ConsoleEmailProvider>();
                    break;

                default:
                    services.AddSingleton<IEmailProvider, ConsoleEmailProvider>();
                    break;
            }

            services.AddScoped<IEmailService, EmailService>();

            return services;
        }
    }
}
