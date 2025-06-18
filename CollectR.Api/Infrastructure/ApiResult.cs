using CollectR.Application.Common.Errors;
using CollectR.Application.Common.Result;

namespace CollectR.Api.Infrastructure;

public static class ApiResult
{
    public static IResult FromResult<T>(Result<T> result)
    {
        return result.Match(onSuccess: value => Results.Ok(value), onFailure: MapErrorToResponse);
    }

    public static IResult FromResult<T>(Result<T> result, Func<T, IResult> onSuccess)
    {
        return result.Match(onSuccess: onSuccess, onFailure: MapErrorToResponse);
    }

    private static IResult MapErrorToResponse(Error error)
    {
        return error.Code switch
        {
            "Entity.NotFound" => Results.NotFound(error.Description),
            "Entity.OneOrMoreDoesNotExist" => Results.NotFound(error.Description),
            "Entity.HasAssignedEntities" => Results.BadRequest(error.Description),
            "File.UnsupportedFormat" => Results.BadRequest(error.Description),
            "File.ImportingFailed" => Results.BadRequest(error.Description),
            _ => Results.Problem(error.Description ?? "An error occurred.")
        };
    }
}
