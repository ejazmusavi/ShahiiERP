namespace ShahiiERP.Domain.Entities.Billing;

public class TenantSubscription : BaseEntity
{
    public Guid TenantId { get; set; }
    public Guid SubscriptionPlanId { get; set; }

    public string BillingCurrency { get; set; }
    public int StudentsLicensed { get; set; }

    public DateTime StartedAt { get; set; }
    public DateTime ExpiresAt { get; set; }
    public bool AutoRenew { get; set; } = true;

    public string Status { get; set; } // Active, Trial, Expired, Grace
}
