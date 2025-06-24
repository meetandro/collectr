using CollectR.Api.Infrastructure;
using CollectR.Application.Features.Tags.Commands.CreateTag;
using CollectR.Application.Features.Tags.Commands.DeleteTag;
using CollectR.Application.Features.Tags.Commands.UpdateTag;
using CollectR.Application.Features.Tags.Queries.GetTagById;
using CollectR.Application.Features.Tags.Queries.GetTags;
using MediatR;

namespace CollectR.Api.Endpoints;

public static class TagEndpoints
{
    public static void MapTagEndpoints(this IEndpointRouteBuilder app)
    {
        var root = app.MapGroup("tags");

        root.MapGet("", GetTags);

        root.MapGet("{id}", GetTagById);

        root.MapPost("", CreateTag);

        root.MapPut("{id}", UpdateTag);

        root.MapDelete("{id}", DeleteTag);
    }

    private static async Task<IResult> GetTags(IMediator mediator)
    {
        var result = await mediator.Send(new GetTagsQuery());
        return ApiResult.FromResult(result);
    }

    private static async Task<IResult> GetTagById(Guid id, IMediator mediator)
    {
        var result = await mediator.Send(new GetTagByIdQuery(id));
        return ApiResult.FromResult(result);
    }

    private static async Task<IResult> CreateTag(
        CreateTagCommand command,
        IMediator mediator
    )
    {
        var result = await mediator.Send(command);
        return ApiResult.FromResult(result);
    }

    private static async Task<IResult> UpdateTag(
        UpdateTagCommand command,
        IMediator mediator
    )
    {
        var result = await mediator.Send(command);
        return ApiResult.FromResult(result);
    }

    private static async Task<IResult> DeleteTag(Guid id, IMediator mediator)
    {
        var result = await mediator.Send(new DeleteTagCommand(id));
        return ApiResult.FromResult(result);
    }
}
