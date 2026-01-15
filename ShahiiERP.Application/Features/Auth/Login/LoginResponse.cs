namespace ShahiiERP.Application.Features.Auth.Login;

public class LoginResponse
{
    public string UserName { get; set; }
    public string DisplayName { get; set; }
    public string Role { get; set; }
    public Guid TenantId { get; set; }
    public Guid? CampusId { get; set; }
    public string? Token { get; set; } // JWT for API later
}
