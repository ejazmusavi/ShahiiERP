namespace ShahiiERP.Application.Abstractions;

public interface IEmailService
{
    Task SendWelcomeEmailAsync(string email, string adminName, string schoolName);
    Task SendVerificationEmailAsync(string tenantCode, string email);
    Task SendGenericAsync(string email, string subject, string body);
}
