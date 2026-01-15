using ShahiiERP.Domain.Entities.Billing;
using ShahiiERP.Domain.Models.Onboarding;

namespace ShahiiERP.Application.Abstractions;

public interface ISubscriptionPlanQueryService
{
    Task<List<SubscriptionPlanVM>> GetPlansByBillingCycleAsync(string billingCycle);
    Task<bool> ExistsAsync(Guid planId);
}
