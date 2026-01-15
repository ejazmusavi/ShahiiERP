using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShahiiERP.Application.Abstractions;
using ShahiiERP.Application.Features.Tenants.Register;
using ShahiiERP.Domain.Models.Onboarding;

namespace ShahiiERP.Web.Controllers
{
    [AllowAnonymous]
    public class OnboardingController : Controller
    {
        private readonly ISubscriptionPlanQueryService _planService;
        private readonly IRegisterTenantService _registerService;

        public OnboardingController(
            ISubscriptionPlanQueryService planService,
            IRegisterTenantService registerService)
        {
            _planService = planService;
            _registerService = registerService;
        }

        [HttpGet("/onboarding/pricing")]
        public async Task<IActionResult> Pricing()
        {
            // fetch monthly & yearly plans from DB
            var monthly = await _planService.GetPlansByBillingCycleAsync("Monthly");
            var yearly = await _planService.GetPlansByBillingCycleAsync("Yearly");

            var vm = new PricingVM
            {
                MonthlyPlans = monthly,
                YearlyPlans = yearly
            };

            return View("/Views/Onboarding/Pricing.cshtml", vm);
        }

        [HttpGet("/onboarding/signup")]
        public async Task<IActionResult> Signup(Guid plan)
        {
            // ensure the plan exists
            var exists = await _planService.ExistsAsync(plan);
            if (!exists)
            {
                return RedirectToAction(nameof(Pricing));
            }

            var vm = new RegisterTenantVM
            {
                PlanId = plan
            };

            return View("/Views/Onboarding/Signup.cshtml", vm);
        }

        [HttpPost("/onboarding/signup")]
        public async Task<IActionResult> Signup(RegisterTenantVM model)
        {
            if (!ModelState.IsValid)
            {
                return View("/Views/Onboarding/Signup.cshtml", model);
            }

            var cmd = new TenantRegistrationDto
            {
                PlanId = model.PlanId,
                SchoolName = model.SchoolName,
                AdminName = model.AdminName,
                Email = model.Email,
                Password = model.Password,
                Phone = model.Phone
            };

            var result = await _registerService.RegisterAsync(cmd);

            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View("/Views/Onboarding/Signup.cshtml", model);
            }

            // send to check-email screen
            return RedirectToAction("CheckEmail", new { email = model.Email });
        }

        [HttpGet("/onboarding/check-email")]
        public IActionResult CheckEmail(string email)
        {
            ViewBag.Email = email;
            return View("/Views/Onboarding/CheckEmail.cshtml");
        }
    }
}
