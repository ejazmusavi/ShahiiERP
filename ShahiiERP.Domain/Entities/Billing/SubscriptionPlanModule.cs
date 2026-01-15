namespace ShahiiERP.Domain.Entities.Billing;

public class SubscriptionPlanModule
{
    public Guid SubscriptionPlanId { get; set; }
    public SubscriptionPlan SubscriptionPlan { get; set; }

    public Guid ModuleId { get; set; }
    public Module Module { get; set; }
}
