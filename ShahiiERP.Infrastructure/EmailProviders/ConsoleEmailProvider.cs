using ShahiiERP.Application.Abstractions;

public class ConsoleEmailProvider : IEmailProvider
{
    public Task SendAsync(string to, string subject, string body, bool isHtml = true)
    {
        Console.WriteLine("==== EMAIL ====");
        Console.WriteLine($"To: {to}");
        Console.WriteLine($"Subject: {subject}");
        Console.WriteLine($"Body:\n{body}");
        Console.WriteLine("================");
        return Task.CompletedTask;
    }
}
