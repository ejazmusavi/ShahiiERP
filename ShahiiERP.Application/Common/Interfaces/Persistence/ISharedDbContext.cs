using Microsoft.EntityFrameworkCore;
using ShahiiERP.Domain.Entities.Billing;
using ShahiiERP.Domain.Entities.Identity;
using ShahiiERP.Domain.Entities.Tenants;
using ShahiiERP.Shared.Entities;

namespace ShahiiERP.Application.Common.Interfaces.Persistence
{
    public interface ISharedDbContext
    {
        DbSet<Tenant> Tenants { get; }
         DbSet<Campus> Campuses { get; set; }

         DbSet<User> Users { get; set; }
         DbSet<Role> Roles { get; set; }
        DbSet<Permission> Permissions { get; set; }
        DbSet<RolePermission> RolePermissions { get; set; }

        DbSet<Module> Modules { get; set; }
        DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }
        DbSet<SubscriptionPlanModule> SubscriptionPlanModules { get; set; }
        DbSet<TenantSubscription> TenantSubscriptions { get; set; }
        DbSet<EmailVerification> EmailVerifications { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
