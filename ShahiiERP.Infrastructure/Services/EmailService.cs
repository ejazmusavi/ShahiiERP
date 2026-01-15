using ShahiiERP.Application.Abstractions;

public class EmailService : IEmailService
{
    private readonly IEmailProvider _provider;

    public EmailService(IEmailProvider provider)
    {
        _provider = provider;
    }

    public Task SendGenericAsync(string email, string subject, string body)
        => _provider.SendAsync(email, subject, body, isHtml: true);

    public Task SendWelcomeEmailAsync(string email, string adminName, string schoolName)
    {
        var subject = $"Welcome to Shahii ERP";
        var body = $@"
            <h2>Welcome {adminName},</h2>
            <p>Your school <strong>{schoolName}</strong> has been successfully registered.</p>
            <p>Please verify your email to activate your account.</p>";
        return _provider.SendAsync(email, subject, body);
    }

    public Task SendVerificationEmailAsync(string tenantCode, string email)
    {
        var subject = "Verify your Shahii ERP account";

        var link = $"https://your-domain.com/verification/confirm?tenant={tenantCode}";

        var body = $@"
            <p>Click the link below to verify your account:</p>
            <p><a href=""{link}"">Verify my email</a></p>";

        return _provider.SendAsync(email, subject, body);
    }
}
