using ShahiiERP.Domain.Tenants;

namespace ShahiiERP.Domain.Entities.Tenants;

public class Tenant : BaseEntity
{
    public string Name { get; set; }
    public string Code { get; set; } // slug e.g. allied, beaconhouse
    public TenantDatabaseMode DatabaseMode { get; set; } // Shared or Dedicated

    public string? ConnectionString { get; set; } // for dedicated DB

    public bool IsActive { get; set; } = true;

    public string BillingCurrency { get; set; }
    public string BillingCountry { get; set; }

    public DateTime? TrialEndsAt { get; set; }
    public Guid? SubscriptionPlanId { get; set; }

    public string? LogoUrl { get; set; }
    public string? Website { get; set; }
    public string? PrimaryContactEmail { get; set; }
    public string? PrimaryContactPhone { get; set; }
    public string? Address { get; set; }

    public ICollection<Campus> Campuses { get; set; }
}
