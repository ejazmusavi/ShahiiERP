using Microsoft.EntityFrameworkCore;
using ShahiiERP.Application.Common.Interfaces.Persistence;
using ShahiiERP.Domain.Entities;
using ShahiiERP.Domain.Entities.Billing;
using ShahiiERP.Domain.Entities.Identity;
using ShahiiERP.Domain.Entities.Tenants;
using ShahiiERP.Shared.Entities;
using System.Reflection.Emit;

namespace ShahiiERP.Infrastructure.Persistence.Contexts;

public class SharedDbContext : DbContext, ISharedDbContext
{
    public SharedDbContext(DbContextOptions<SharedDbContext> options)
        : base(options)
    {
    }

    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<Campus> Campuses { get; set; }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }

    public DbSet<Module> Modules { get; set; }
    public DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }
    public DbSet<SubscriptionPlanModule> SubscriptionPlanModules { get; set; }
    public DbSet<TenantSubscription> TenantSubscriptions { get; set; }
    public DbSet<EmailVerification> EmailVerifications { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(SharedDbContext).Assembly);
        // global filter: soft delete
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
            {
                var method = typeof(SharedDbContext)
                    .GetMethod(nameof(ApplySoftDeleteFilter), System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)!
                    .MakeGenericMethod(entityType.ClrType);

                method.Invoke(null, new object[] { builder });
            }
        }
        builder.Entity<RolePermission>().HasKey(x => new { x.RoleId, x.PermissionId });
        builder.Entity<SubscriptionPlanModule>().HasKey(x => new { x.SubscriptionPlanId, x.ModuleId });
    }

    private static void ApplySoftDeleteFilter<TEntity>(ModelBuilder modelBuilder)
            where TEntity : BaseEntity
    {
        modelBuilder.Entity<TEntity>().HasQueryFilter(x => !x.IsDeleted);
    }
}
