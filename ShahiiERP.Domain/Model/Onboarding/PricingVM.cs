namespace ShahiiERP.Domain.Models.Onboarding;

public class PricingVM
{
    public List<SubscriptionPlanVM> MonthlyPlans { get; set; } = new();
    public List<SubscriptionPlanVM> YearlyPlans { get; set; } = new();
}
