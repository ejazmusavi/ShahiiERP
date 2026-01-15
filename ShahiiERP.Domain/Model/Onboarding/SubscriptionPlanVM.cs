namespace ShahiiERP.Domain.Models.Onboarding;

public class SubscriptionPlanVM
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string BillingCycle { get; set; } = default!; // Monthly or Yearly
    public decimal Price { get; set; }
    public int? StudentLimit { get; set; }
    public bool IsFreeTier { get; set; }
    public string? Description { get; set; }
}
