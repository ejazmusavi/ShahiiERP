public class SubscriptionPlanDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public decimal PricePerStudent { get; set; }
    public string BillingCycle { get; set; } = default!;
    public bool IsFreeTier { get; set; }
    public int? StudentLimit { get; set; }
}
