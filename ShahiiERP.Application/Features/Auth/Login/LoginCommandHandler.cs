using MediatR;
using ShahiiERP.Application.Abstractions;
using ShahiiERP.Application.Common.Interfaces.Tenancy;
using ShahiiERP.Application.Common.Results;

namespace ShahiiERP.Application.Features.Auth.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<LoginResponse>>
{
    private readonly ITenantResolver _tenantResolver;
    private readonly IAuthService _authService;

    public LoginCommandHandler(ITenantResolver tenantResolver, IAuthService authService)
    {
        _tenantResolver = tenantResolver;
        _authService = authService;
    }

    public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var tenant = await _tenantResolver.ResolveAsync();

        if (tenant == null)
            return Result<LoginResponse>.Fail("Invalid tenant");

        var user = await _authService.ValidateUserAsync(request.UserName, request.Password, tenant);

        if (user == null)
            return Result<LoginResponse>.Fail("Invalid username or password");

        var token = await _authService.GenerateJwtTokenAsync(user, tenant);

        return Result<LoginResponse>.Ok(new LoginResponse
        {
            UserName = user.UserName,
            DisplayName = $"{user.FirstName} {user.LastName}",
            Role = user.Role.Name,
            TenantId = tenant.TenantId,
            CampusId = user.CampusId,
            Token = token
        }, "Login successful");
    }
}
