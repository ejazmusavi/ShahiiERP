using System.Net;
using System.Net.Mail;
using ShahiiERP.Application.Abstractions;

public class SmtpEmailProvider : IEmailProvider
{
    private readonly SmtpSettings _settings;

    public SmtpEmailProvider(SmtpSettings settings)
    {
        _settings = settings;
    }

    public async Task SendAsync(string to, string subject, string body, bool isHtml = true)
    {
        using var client = new SmtpClient(_settings.Host, _settings.Port)
        {
            Credentials = new NetworkCredential(_settings.User, _settings.Password),
            EnableSsl = _settings.UseSsl
        };

        var mail = new MailMessage(_settings.From, to, subject, body)
        {
            IsBodyHtml = isHtml
        };

        await client.SendMailAsync(mail);
    }
}

public record SmtpSettings(
    string Host,
    int Port,
    string User,
    string Password,
    string From,
    bool UseSsl
);
