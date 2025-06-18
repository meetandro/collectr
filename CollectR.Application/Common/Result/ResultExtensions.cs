using CollectR.Application.Common.Errors;

namespace CollectR.Application.Common.Result;

public static class ResultExtensions
{
    public static TResult Match<TValue, TResult>(
        this Result<TValue> result,
        Func<TValue, TResult> onSuccess,
        Func<Error, TResult> onFailure
    ) => result.IsSuccess ? onSuccess(result.Value) : onFailure(result.Error);
}
