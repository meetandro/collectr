using CollectR.Application.Common.Result;

namespace CollectR.Application.Common.Errors;

public static class ErrorExtensions
{
    public static Result<T> ToFailure<T>(this Error error) => Result.Result.Failure<T>(error);
}
