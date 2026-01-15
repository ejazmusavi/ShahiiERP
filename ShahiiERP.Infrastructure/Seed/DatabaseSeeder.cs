using ShahiiERP.Domain.Entities.Identity;
using ShahiiERP.Domain.Entities.Tenants;
using ShahiiERP.Infrastructure.Persistence.Contexts;
using ShahiiERP.Infrastructure.Security;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(SharedDbContext db)
    {
        if (!db.Tenants.Any())
        {
            var tenant = new Tenant
            {
                Name = "Demo School Group",
                Code = "demo",
                DatabaseMode = ShahiiERP.Domain.Tenants.TenantDatabaseMode.Shared,
                BillingCurrency = "PKR",
                BillingCountry = "PK",
                TrialEndsAt = DateTime.UtcNow.AddDays(30)
            };
            db.Tenants.Add(tenant);
            await db.SaveChangesAsync();
        }

        if (!db.Roles.Any())
        {
            db.Roles.Add(new Role { Name = "Admin", IsSystemRole = true });
            db.Roles.Add(new Role { Name = "Teacher", IsSystemRole = true });
            db.Roles.Add(new Role { Name = "Student", IsSystemRole = true });
            db.Roles.Add(new Role { Name = "Parent", IsSystemRole = true });
            db.Roles.Add(new Role { Name = "HR", IsSystemRole = true });
            db.Roles.Add(new Role { Name = "Accountant", IsSystemRole = true });

            await db.SaveChangesAsync();
        }

        if (!db.Users.Any())
        {
            var adminRoleId = db.Roles.First(x => x.Name == "Admin").Id;
            var(salt,hash) = PasswordHasher.HashPassword("Admin@123");
            var admin = new User
            {
                UserName = "admin",
                Email = "admin@demo.com",
                FirstName = "System",
                LastName = "Admin",
                PasswordHash = hash, // we hash later
                PasswordSalt = salt,
                RoleId = adminRoleId,
                IsActive = true
            };

            db.Users.Add(admin);
            await db.SaveChangesAsync();
        }
    }
}
