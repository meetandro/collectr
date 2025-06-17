using CollectR.Api.Infrastructure;
using CollectR.Application.Common;
using CollectR.Application.Contracts.Services;
using CollectR.Application.Features.Collections.Commands.CreateCollection;
using CollectR.Application.Features.Collections.Commands.DeleteCollection;
using CollectR.Application.Features.Collections.Commands.ImportCollection;
using CollectR.Application.Features.Collections.Commands.UpdateCollection;
using CollectR.Application.Features.Collections.Queries.ExportCollection;
using CollectR.Application.Features.Collections.Queries.GetCollectiblesForCollection;
using CollectR.Application.Features.Collections.Queries.GetCollectionById;
using CollectR.Application.Features.Collections.Queries.GetCollections;
using CollectR.Application.Features.Collections.Queries.GetTagsForCollection;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CollectR.Api.Endpoints;

public static class CollectionEndpoints
{
    public static void MapCollectionEndpoints(this IEndpointRouteBuilder app)
    {
        var root = app.MapGroup("collections");

        root.MapGet("", GetAllCollections);

        root.MapGet("/{id}", GetCollectionById);

        root.MapGet("/{id}/collectibles", GetCollectiblesForCollection);

        root.MapGet("/{id}/tags", GetTagsForCollection);

        root.MapGet("/{id}/export", ExportCollection);

        root.MapPost("/import", ImportCollection).DisableAntiforgery();

        root.MapPost("", CreateCollection);

        root.MapPut("/{id}", UpdateCollection);

        root.MapDelete("/{id}", DeleteCollection);
    }

    public static async Task<IResult> GetAllCollections(IMediator mediator)
    {
        var result = await mediator.Send(new GetCollectionsQuery());
        return Results.Ok(result);
    }

    public static async Task<IResult> GetCollectionById(Guid id, IMediator mediator)
    {
        var result = await mediator.Send(new GetCollectionByIdQuery(id));
        return ApiResult.FromResult(result);
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
        var result = await mediator.Send(new GetTagsForCollectionQuery(id));
        return Results.Ok(result);
    }

    public static async Task<IResult> ExportCollection(Guid id, string format, IMediator mediator)
    {
        var result = await mediator.Send(new ExportCollectionQuery(id, format));
        return result.Match(
            onSuccess: value => Results.File(value.FileContents, value.ContentType, value.FileName),
            onFailure: error => Results.BadRequest(result.Error)
        );
    }

    public static async Task<IResult> ImportCollection(
        IFormFile file,
        IFileService fileService,
        IMediator mediator
    )
    {
        var result = await mediator.Send(
            new ImportCollectionCommand(
                await fileService.ConvertToByteArrayAsync(file),
                file.FileName
            )
        );
        return ApiResult.FromResult(result);
    }

    public static async Task<IResult> CreateCollection(
        [FromBody] CreateCollectionCommand command,
        IMediator mediator
    )
    {
        var result = await mediator.Send(command);
        return ApiResult.FromResult(result);
    }

    public static async Task<IResult> UpdateCollection(
        [FromBody] UpdateCollectionCommand command,
        IMediator mediator
    )
    {
        var result = await mediator.Send(command);
        return ApiResult.FromResult(result);
    }

    public static async Task<IResult> DeleteCollection(Guid id, IMediator mediator)
    {
        var result = await mediator.Send(new DeleteCollectionCommand(id));
        return ApiResult.FromResult(result);
    }
}
