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
    public static void MapCollectibleEndpoints(this WebApplication app)
    {
        var root = app.MapGroup("/api/collectibles");

        root.MapGet("", GetAllCollectibles);

        root.MapGet("/{id}", GetCollectibleById);

        root.MapPost("", CreateCollectible).DisableAntiforgery();

        root.MapPut("/{id}", UpdateCollectible).DisableAntiforgery();

        root.MapDelete("/{id}", DeleteCollectible);

        root.MapPut("/{id}/tags", UpdateCollectibleTags);
    }

    public static async Task<IResult> GetAllCollectibles(IMediator mediator)
    {
        var result = await mediator.Send(new GetCollectiblesQuery());
        return Results.Ok(result);
    }

    public static async Task<IResult> GetCollectibleById(Guid id, IMediator mediator)
    {
        var result = await mediator.Send(new GetCollectibleByIdQuery(id));
        return Results.Ok(result);
    }

    public static async Task<IResult> CreateCollectible(
        [AsParameters] CreateCollectibleCommand command,
        IMediator mediator
    )
    {
        var result = await mediator.Send(command);
        return Results.Ok(result);
    }

    public static async Task<IResult> UpdateCollectible(
        [AsParameters] UpdateCollectibleCommand command,
        IMediator mediator
    )
    {
        var result = await mediator.Send(command);
        return Results.Ok(result);
    }

    public static async Task<IResult> DeleteCollectible(Guid id, IMediator mediator)
    {
        var result = await mediator.Send(new DeleteCollectibleCommand(id));
        return Results.Ok(result);
    }

    public static async Task<IResult> UpdateCollectibleTags(UpdateCollectibleTagsCommand command, IMediator mediator)
    {
        var result = await mediator.Send(command);
        return Results.Ok(result);
    }
}
