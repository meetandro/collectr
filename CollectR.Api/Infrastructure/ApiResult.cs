using CollectR.Application.Common;

namespace CollectR.Api.Infrastructure;

public static class ApiResult
{
    public static IResult FromResult<T>(Result<T> result)
    {
        return result.Match(
            onSuccess: value => Results.Ok(value),
            onFailure: MapErrorToResponse
        );
    }

    private static IResult MapErrorToResponse(Error error)
    {
        return error.Code switch
        {
            "Entity.NotFound" => Results.NotFound(error.Description),
            "Entity.OneOrMoreDoesntExist" => Results.NotFound(error.Description),
            _ => Results.Problem(error.Description ?? "An error occurred")
        };
    }
}
