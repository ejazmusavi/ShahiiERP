namespace ShahiiERP.Application.Common;

public class OperationResult
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;

    public static OperationResult Ok(string message = "OK")
        => new() { Success = true, Message = message };

    public static OperationResult Fail(string message)
        => new() { Success = false, Message = message };
}
