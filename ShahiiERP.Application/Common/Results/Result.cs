namespace ShahiiERP.Application.Common.Results;

public class Result<T>
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }
    public List<string>? Errors { get; set; }

    public static Result<T> Ok(T data, string? message = null)
        => new Result<T> { Success = true, Data = data, Message = message };

    public static Result<T> Fail(List<string> errors, string? message = null)
        => new Result<T> { Success = false, Errors = errors, Message = message };

    public static Result<T> Fail(string error, string? message = null)
        => new Result<T>
        { Success = false, Errors = new List<string> { error }, Message = message };
}
