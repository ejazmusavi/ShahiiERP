namespace ShahiiERP.Domain.Entities.Billing;

public class SubscriptionPlan : BaseEntity
{
    public string Name { get; set; }
    public decimal PricePerStudent { get; set; }
    public string BillingCycle { get; set; } // Monthly / Annual

    public bool IsFreeTier { get; set; }
    public int? StudentLimit { get; set; }

    public ICollection<SubscriptionPlanModule> Modules { get; set; }
}
