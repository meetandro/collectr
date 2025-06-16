using CollectR.Application.Features.Collections.Commands.CreateCollection;
using CollectR.Application.Features.Collections.Commands.DeleteCollection;
using CollectR.Application.Features.Collections.Commands.UpdateCollection;
using CollectR.Application.Features.Collections.Queries.ExportCollection;
using CollectR.Application.Features.Collections.Queries.GetCollectiblesForCollection;
using CollectR.Application.Features.Collections.Queries.GetCollectionById;
using CollectR.Application.Features.Collections.Queries.GetCollections;
using CollectR.Application.Features.Collections.Queries.GetTagsForCollection;
using CollectR.Application.Features.Collections.Queries.ImportCollection;
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

        root.MapGet("/{id}/collectibles", GetCollectiblesForCollection);

        root.MapGet("/{id}/tags", GetTagsForCollection);

        root.MapGet("/{id}/export", ExportCollection);

        root.MapPost("/import", ImportCollection).DisableAntiforgery();
    }

    public static async Task<IResult> GetAllCollections(IMediator mediator)
    {
        var result = await mediator.Send(new GetCollectionsQuery());
        return Results.Ok(result);
    }

    public static async Task<IResult> GetCollectionById(Guid id, IMediator mediator)
    {
        var result = await mediator.Send(new GetCollectionByIdQuery(id));
        return Results.Ok(result);
    }

    public static async Task<IResult> CreateCollection(
        [FromBody] CreateCollectionCommand command,
        IMediator mediator
    )
    {
        var result = await mediator.Send(command);
        return Results.Ok(result);
    }

    public static async Task<IResult> UpdateCollection(
        [FromBody] UpdateCollectionCommand command,
        IMediator mediator
    )
    {
        var result = await mediator.Send(command);
        return Results.Ok(result);
    }

    public static async Task<IResult> DeleteCollection(Guid id, IMediator mediator)
    {
        var result = await mediator.Send(new DeleteCollectionCommand(id));
        return Results.Ok(result);
    }

    public static async Task<IResult> GetCollectiblesForCollection(
        [AsParameters] GetCollectiblesForCollectionQuery query,
        IMediator mediator
    )
    {
        var result = await mediator.Send(query);
        return Results.Ok(result);
    }

    public static async Task<IResult> GetTagsForCollection(Guid id, IMediator mediator)
    {
        var query = new GetTagsForCollectionQuery(id);
        var result = await mediator.Send(query);
        return Results.Ok(result);
    }

    public static async Task<IResult> ExportCollection(Guid id, string format, IMediator mediator)
    {
        var query = new ExportCollectionQuery(id, format);
        var result = await mediator.Send(query);
        return Results.File(result.Value.FileContents, result.Value.ContentType, result.Value.FileName);
    }

    public static async Task<IResult> ImportCollection(IFormFile file, IMediator mediator)
    {
        var query = new ImportCollectionCommand(await ToByteArrayAsync(file), file.FileName);
        var result = await mediator.Send(query);
        return Results.Ok();
    }

    public static async Task<byte[]> ToByteArrayAsync(IFormFile file)
    {
        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        return memoryStream.ToArray();
    }
}
