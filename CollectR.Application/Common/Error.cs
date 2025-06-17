namespace CollectR.Application.Common;

public sealed record Error(string Code, string? Description = null)
{
    public static readonly Error None = new(string.Empty);
}

public static class ErrorExtensions
{
    public static Result<T> ToFailure<T>(this Error error) => Result.Failure<T>(error);
}
