using MediatR;
using ShahiiERP.Application.Common.Results;

namespace ShahiiERP.Application.Features.Auth.Login;

public record LoginCommand(string UserName, string Password, string TenantCode)
    : IRequest<Result<LoginResponse>>;
