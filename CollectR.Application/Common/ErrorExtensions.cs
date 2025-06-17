namespace CollectR.Application.Common;

public static class ErrorExtensions
{
    public static Result<T> ToFailure<T>(this Error error) => Result.Failure<T>(error);
}
