using CollectR.Application.Features.Collections.Commands.CreateCollection;
using CollectR.Application.Features.Collections.Commands.DeleteCollection;
using CollectR.Application.Features.Collections.Commands.UpdateCollection;
using CollectR.Application.Features.Collections.Queries.GetAllCollections;
using CollectR.Application.Features.Collections.Queries.GetCollectionById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CollectR.Api.Endpoints;

public static class CollectionEndpoints
{
    public static void MapCollectionEndpoints(this WebApplication app)
    {
        var root = app.MapGroup("/api/collections");

        root.MapGet("", GetAllCollections);

        root.MapGet("/{id}", GetCollectionById);

        root.MapPost("", CreateCollection);

        root.MapPut("/{id}", UpdateCollection);

        root.MapDelete("/{id}", DeleteCollection);
    }

    public static async Task<IResult> GetAllCollections(IMediator mediator)
    {
        var result = await mediator.Send(new GetAllCollectionsQuery());
        return Results.Ok(result);
    }

    public static async Task<IResult> GetCollectionById(int id, IMediator mediator)
    {
        var result = await mediator.Send(new GetCollectionByIdQuery(id));
        return Results.Ok(result);
    }

    public static async Task<IResult> CreateCollection([FromBody] CreateCollectionCommand command, IMediator mediator)
    {
        var result = await mediator.Send(command);
        return Results.Ok(result);
    }

    public static async Task<IResult> UpdateCollection([FromBody] UpdateCollectionCommand command, IMediator mediator)
    {
        var result = await mediator.Send(command);
        return Results.Ok(result);
    }

    public static async Task<IResult> DeleteCollection(int id, IMediator mediator)
    {
        var result = await mediator.Send(new DeleteCollectionCommand(id));
        return Results.Ok(result);
    }
}
