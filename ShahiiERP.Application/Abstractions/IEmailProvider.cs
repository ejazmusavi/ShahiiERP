namespace ShahiiERP.Application.Abstractions;

public interface IEmailProvider
{
    Task SendAsync(string to, string subject, string body, bool isHtml = true);
}
