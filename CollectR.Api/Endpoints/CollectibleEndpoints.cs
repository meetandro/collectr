﻿using CollectR.Api.Infrastructure;
using CollectR.Application.Features.Collectibles.Commands.CreateCollectible;
using CollectR.Application.Features.Collectibles.Commands.DeleteCollectible;
using CollectR.Application.Features.Collectibles.Commands.UpdateCollectible;
using CollectR.Application.Features.Collectibles.Commands.UpdateCollectibleTags;
using CollectR.Application.Features.Collectibles.Queries.GetCollectibleById;
using CollectR.Application.Features.Collectibles.Queries.GetCollectibles;
using MediatR;

namespace CollectR.Api.Endpoints;

public static class CollectibleEndpoints
{
    public static void MapCollectibleEndpoints(this IEndpointRouteBuilder app)
    {
        var root = app.MapGroup("collectibles");

        root.MapGet("", GetCollectibles);

        root.MapGet("{id}", GetCollectibleById);

        root.MapPost("", CreateCollectible).DisableAntiforgery();

        root.MapPut("{id}", UpdateCollectible).DisableAntiforgery();

        root.MapPut("{id}/tags", UpdateCollectibleTags);

        root.MapDelete("{id}", DeleteCollectible);
    }

    private static async Task<IResult> GetCollectibles(IMediator mediator)
    {
        var result = await mediator.Send(new GetCollectiblesQuery());
        return ApiResult.FromResult(result);
    }

    private static async Task<IResult> GetCollectibleById(Guid id, IMediator mediator)
    {
        var result = await mediator.Send(new GetCollectibleByIdQuery(id));
        return ApiResult.FromResult(result);
    }

    private static async Task<IResult> CreateCollectible(
        [AsParameters] CreateCollectibleCommand command,
        IMediator mediator
    )
    {
        var result = await mediator.Send(command);
        return ApiResult.FromResult(result);
    }

    private static async Task<IResult> UpdateCollectible(
        [AsParameters] UpdateCollectibleCommand command,
        IMediator mediator
    )
    {
        var result = await mediator.Send(command);
        return ApiResult.FromResult(result);
    }

    private static async Task<IResult> UpdateCollectibleTags(
        UpdateCollectibleTagsCommand command,
        IMediator mediator
    )
    {
        var result = await mediator.Send(command);
        return ApiResult.FromResult(result);
    }

    private static async Task<IResult> DeleteCollectible(Guid id, IMediator mediator)
    {
        var result = await mediator.Send(new DeleteCollectibleCommand(id));
        return ApiResult.FromResult(result);
    }
}
