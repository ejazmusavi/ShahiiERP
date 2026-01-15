using Microsoft.EntityFrameworkCore;
using ShahiiERP.Application.Abstractions;
using ShahiiERP.Domain.Models.Onboarding;
using ShahiiERP.Infrastructure.Persistence.Contexts;

public class SubscriptionPlanQueryService : ISubscriptionPlanQueryService
{
    private readonly SharedDbContext _db;

    public SubscriptionPlanQueryService(SharedDbContext db)
    {
        _db = db;
    }

    public async Task<List<SubscriptionPlanVM>> GetPlansByBillingCycleAsync(string billingCycle)
    {
        return await _db.SubscriptionPlans
            .Where(x => !x.IsDeleted && x.BillingCycle == billingCycle)
            .OrderBy(x => x.IsFreeTier ? 0 : 1)
            .Select(x => new SubscriptionPlanVM
            {
                Id = x.Id,
                Name = x.Name,
                BillingCycle = x.BillingCycle,
                Price = x.PricePerStudent,
                StudentLimit = x.StudentLimit,
                IsFreeTier = x.IsFreeTier,
                Description = null
            })
            .ToListAsync();
    }

    public async Task<bool> ExistsAsync(Guid planId)
    {
        return await _db.SubscriptionPlans.AnyAsync(x => x.Id == planId && !x.IsDeleted);
    }
}
