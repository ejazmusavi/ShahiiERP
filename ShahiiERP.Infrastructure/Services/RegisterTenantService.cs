using ShahiiERP.Application.Abstractions;
using ShahiiERP.Application.Common;
using ShahiiERP.Application.Features.Tenants.Register;
using ShahiiERP.Infrastructure.Persistence.Contexts;

//using ShahiiERP.Application.Features.Tenants.Register;
using ShahiiERP.Infrastructure.Services;

public class RegisterTenantService : IRegisterTenantService
{
    private readonly ITenantProvisioningService _provisioning;
    private readonly IEmailService _emailService;
    private readonly SharedDbContext _db;

    public RegisterTenantService(
        ITenantProvisioningService provisioning,
        IEmailService emailService,
        SharedDbContext db)
    {
        _provisioning = provisioning;
        _emailService = emailService;
        _db = db;
    }

    public async Task<OperationResult> RegisterAsync(TenantRegistrationDto cmd)
    {
        // validate unique email
        if (_db.Users.Any(x => x.Email == cmd.Email))
            return OperationResult.Fail("Email already exists.");
        
        // provision tenant
        var provisionResult = await _provisioning.ProvisionAsync(cmd );

        if (!provisionResult.Success)
            return OperationResult.Fail(provisionResult.Errors?.FirstOrDefault()??"");

        // send welcome + verification emails
        await _emailService.SendWelcomeEmailAsync(cmd.Email, cmd.AdminName, cmd.SchoolName);
        await _emailService.SendVerificationEmailAsync(provisionResult.TenantCode, cmd.Email);

        return OperationResult.Ok("Tenant provisioned.");
    }
}
