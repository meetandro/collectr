using CollectR.Application.Features.Tags.Commands.CreateTag;
using CollectR.Application.Features.Tags.Commands.DeleteTag;
using CollectR.Application.Features.Tags.Commands.UpdateTag;
using CollectR.Application.Features.Tags.Queries.GetAllTags;
using CollectR.Application.Features.Tags.Queries.GetTagById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CollectR.Api.Endpoints;

public static class TagEndpoints
{
    public static void MapTagEndpoints(this WebApplication app)
    {
        var root = app.MapGroup("/api/tags");

        root.MapGet("", GetAllTags);

        root.MapGet("/{id}", GetTagById);

        root.MapPost("", CreateTag);

        root.MapPut("/{id}", UpdateTag);

        root.MapDelete("/{id}", DeleteTag);
    }

    public static async Task<IResult> GetAllTags(IMediator mediator)
    {
        var result = await mediator.Send(new GetAllTagsQuery());
        return Results.Ok(result);
    }

    public static async Task<IResult> GetTagById(int id, IMediator mediator)
    {
        var result = await mediator.Send(new GetTagByIdQuery(id));
        return Results.Ok(result);
    }

    public static async Task<IResult> CreateTag([FromBody] CreateTagCommand command, IMediator mediator)
    {
        var result = await mediator.Send(command);
        return Results.Ok(result);
    }

    public static async Task<IResult> UpdateTag([FromBody] UpdateTagCommand command, IMediator mediator)
    {
        var result = await mediator.Send(command);
        return Results.Ok(result);
    }

    public static async Task<IResult> DeleteTag(int id, IMediator mediator)
    {
        var result = await mediator.Send(new DeleteTagCommand(id));
        return Results.Ok(result);
    }
}
