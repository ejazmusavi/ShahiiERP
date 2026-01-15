using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShahiiERP.Application.Abstractions;
using ShahiiERP.Application.Common.Interfaces.Persistence;
using ShahiiERP.Application.Features.Tenants.Register;
using ShahiiERP.Domain.Entities.Identity;
using ShahiiERP.Domain.Entities.Tenants;
using ShahiiERP.Infrastructure.Persistence;
using ShahiiERP.Infrastructure.Persistence.Contexts;
using ShahiiERP.Infrastructure.Persistence.TenantDb;
using ShahiiERP.Infrastructure.Security;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ShahiiERP.Infrastructure.Services;

public class TenantProvisioningService : ITenantProvisioningService
{
    private readonly SharedDbContext _sharedDb;
    private readonly ITenantDbContextFactory _tenantDbFactory;

    public TenantProvisioningService(
        SharedDbContext sharedDb,
        ITenantDbContextFactory tenantDbFactory)
    {
        _sharedDb = sharedDb;
        _tenantDbFactory = tenantDbFactory;
    }

    public async Task<TenantRegistrationResult> ProvisionAsync(TenantRegistrationDto dto)
    {
        var result = new TenantRegistrationResult();

        // generate tenant code from school name
        var code = GenerateTenantCode(dto.SchoolName);

        // ensure uniqueness
        if (await _sharedDb.Tenants.AnyAsync(x => x.Code == code))
        {
            result.Success = false;
            result.Errors = new() { "Tenant code already exists" };
            return result;
        }

        var tenantId = Guid.NewGuid();

        // --- INSERT TENANT ---
        var tenant = new Tenant
        {
            Id = tenantId,
            Name = dto.SchoolName,
            Code = code,
            DatabaseMode = Domain.Tenants.TenantDatabaseMode.Shared, // "Shared", // From Q1 = B
            ConnectionString = null,
            IsActive = true,
            BillingCurrency = "PKR",
            BillingCountry = "PK",
            TrialEndsAt = DateTime.UtcNow.AddDays(14),
            SubscriptionPlanId = null,
            LogoUrl = null,
            Website = null,
            PrimaryContactEmail = dto.Email,
            PrimaryContactPhone = null,
            Address = null,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = null,
            UpdatedAt = null,
            UpdatedBy = null,
            IsDeleted = false
        };

        _sharedDb.Tenants.Add(tenant);

        // --- CREATE ADMIN ROLE FOR TENANT ---
        var adminRoleId = Guid.NewGuid();

        var adminRole = new Role
        {
            Id = adminRoleId,
            TenantId = tenantId,
            Name = "Admin",
            Description = "Default Administrator Role",
            IsSystemRole = false, // Q2 = B
            CreatedAt = DateTime.UtcNow,
            CreatedBy = null,
            UpdatedAt = null,
            UpdatedBy = null,
            IsDeleted = false
        };

        _sharedDb.Roles.Add(adminRole);

        // --- SEED BASIC PERMISSIONS (OPTIONAL PLACEHOLDER) ---
        // later we can map permissions from Modules table
        // for now: no-op or map default modules

        // --- CREATE MAIN CAMPUS ---
        var campusId = Guid.NewGuid();
        var campus = new Campus
        {
            Id = campusId,
            TenantId = tenantId,
            Name = "Main Campus",
            Code = "MAIN",
            Address = null,
            City = null,
            Country = null,
            Phone = null,
            Email = null,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = null,
            UpdatedAt = null,
            UpdatedBy = null,
            IsDeleted = false
        };

        _sharedDb.Campuses.Add(campus);

        // --- CREATE FIRST ADMIN USER ---

        var (hash,salt) = PasswordHasher.HashPassword(dto.Password);
        var userId = Guid.NewGuid();
        var user = new User
        {
            Id = userId,
            TenantId = tenantId,
            CampusId = campusId,
            UserName = dto.Email.ToLower(),
            Email = dto.Email.ToLower(),
            FirstName = dto.AdminName,
            LastName = "",
            PhoneNumber = null,
            PasswordHash = hash,
            PasswordSalt = salt,
            IsActive = true,
            LastLoginAt = null,
            RoleId = adminRoleId,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = null,
            UpdatedAt = null,
            UpdatedBy = null,
            IsDeleted = false
        };
        
        _sharedDb.Users.Add(user);

        // persist all shared DB inserts
        await _sharedDb.SaveChangesAsync();


        
        var tenantDb = _tenantDbFactory.Create(new Application.Common.Models.TenantContextModel { DatabaseMode = Domain.Tenants.TenantDatabaseMode.Shared });
        
        await TenantDbSeeder.SeedAsync(tenantDb, tenantId);
        result.Success = true;
        result.TenantId = tenantId;
        result.TenantCode = code;
        result.LoginUrl = $"/{code}/login";

        return result;
    }

    private string GenerateTenantCode(string name)
    {
        var normalized = new string(name
            .Where(char.IsLetterOrDigit)
            .ToArray())
            .ToLower();

        return normalized.Length > 3 ? normalized : normalized + "sch";
    }
}
