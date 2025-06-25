using CollectR.Api.Infrastructure;
using CollectR.Application.Features.Collections.Commands.CreateCollection;
using CollectR.Application.Features.Collections.Commands.DeleteCollection;
using CollectR.Application.Features.Collections.Commands.ImportCollection;
using CollectR.Application.Features.Collections.Commands.MergeCollection;
using CollectR.Application.Features.Collections.Commands.UpdateCollection;
using CollectR.Application.Features.Collections.Queries.ExportCollection;
using CollectR.Application.Features.Collections.Queries.GetCollectiblesForCollection;
using CollectR.Application.Features.Collections.Queries.GetCollectionById;
using CollectR.Application.Features.Collections.Queries.GetCollections;
using CollectR.Application.Features.Collections.Queries.GetTagsForCollection;
using MediatR;

namespace CollectR.Api.Endpoints;

public static class CollectionEndpoints
{
    public static void MapCollectionEndpoints(this IEndpointRouteBuilder app)
    {
        var root = app.MapGroup("collections");

        root.MapGet("", GetCollections);

        root.MapGet("{id}", GetCollectionById);

        root.MapGet("{id}/collectibles", GetCollectiblesForCollection);

        root.MapGet("{id}/tags", GetTagsForCollection);

        root.MapGet("{id}/export", ExportCollection);

        root.MapPost("import", ImportCollection).DisableAntiforgery();

        root.MapPost("{id}/merge", MergeCollection).DisableAntiforgery();

        root.MapPost("", CreateCollection);

        root.MapPut("{id}", UpdateCollection);

        root.MapDelete("{id}", DeleteCollection);
    }

    private static async Task<IResult> GetCollections(IMediator mediator)
    {
        var result = await mediator.Send(new GetCollectionsQuery());
        return ApiResult.FromResult(result);
    }

    private static async Task<IResult> GetCollectionById(Guid id, IMediator mediator)
    {
        var result = await mediator.Send(new GetCollectionByIdQuery(id));
        return ApiResult.FromResult(result);
    }

    private static async Task<IResult> GetCollectiblesForCollection(
        [AsParameters] GetCollectiblesForCollectionQuery query,
        IMediator mediator
    )
    {
        var result = await mediator.Send(query);
        return ApiResult.FromResult(result);
    }

    private static async Task<IResult> GetTagsForCollection(Guid id, IMediator mediator)
    {
        var result = await mediator.Send(new GetTagsForCollectionQuery(id));
        return ApiResult.FromResult(result);
    }

    private static async Task<IResult> ExportCollection(
        [AsParameters] ExportCollectionQuery query,
        IMediator mediator
    )
    {
        var result = await mediator.Send(query);
        return ApiResult.FromResult(
            result,
            value => Results.File(value.FileContents, value.ContentType, value.FileName)
        );
    }

    private static async Task<IResult> ImportCollection(
        [AsParameters] ImportCollectionCommand command,
        IMediator mediator
    )
    {
        var result = await mediator.Send(command);
        return ApiResult.FromResult(result);
    }

    private static async Task<IResult> MergeCollection(
        [AsParameters] MergeCollectionCommand command,
        IMediator mediator
    )
    {
        var result = await mediator.Send(command);
        return ApiResult.FromResult(result);
    }

    private static async Task<IResult> CreateCollection(
        CreateCollectionCommand command,
        IMediator mediator
    )
    {
        var result = await mediator.Send(command);
        return ApiResult.FromResult(result);
    }

    private static async Task<IResult> UpdateCollection(
        UpdateCollectionCommand command,
        IMediator mediator
    )
    {
        var result = await mediator.Send(command);
        return ApiResult.FromResult(result);
    }

    private static async Task<IResult> DeleteCollection(Guid id, IMediator mediator)
    {
        var result = await mediator.Send(new DeleteCollectionCommand(id));
        return ApiResult.FromResult(result);
    }
}
