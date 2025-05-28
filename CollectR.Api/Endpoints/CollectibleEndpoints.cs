using CollectR.Application.Features.Collectibles.Commands.CreateCollectible;
using CollectR.Application.Features.Collectibles.Commands.DeleteCollectible;
using CollectR.Application.Features.Collectibles.Commands.UpdateCollectible;
using CollectR.Application.Features.Collectibles.Queries.GetAllCollectibles;
using CollectR.Application.Features.Collectibles.Queries.GetCollectibleById;
using CollectR.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
    }

    public static async Task<IResult> GetAllCollectibles(IMediator mediator)
    {
        var result = await mediator.Send(new GetAllCollectiblesQuery());
        return Results.Ok(result);
    }

    public static async Task<IResult> GetCollectibleById(int id, IMediator mediator)
    {
        var result = await mediator.Send(new GetCollectibleByIdQuery(id));
        return Results.Ok(result);
    }

    /*    public static async Task<IResult> CreateCollectible([FromBody] CreateCollectibleCommand command, IMediator mediator)
        {
            var result = await mediator.Send(command);
            return Results.Ok(result);
        }*/

    public static async Task<IResult> CreateCollectible(
        string Title,
        string? Description,
        string? Currency,
        decimal? Value,
        DateTime? AcquiredDate,
        bool? IsCollected,
        int SortIndex,
        Color? Color,
        Condition? Condition,
        string? Metadata,
        int CategoryId,
        int CollectionId,
        IFormFileCollection Images,
        IMediator mediator
    )
    {
        var result = await mediator.Send(new CreateCollectibleCommand(Title, Description, Currency, Value, AcquiredDate, IsCollected, SortIndex, Color, Condition, Metadata, CategoryId, CollectionId, Images));
        return Results.Ok(result);
    }

    public static async Task<IResult> UpdateCollectible([FromBody] UpdateCollectibleCommand command, IMediator mediator)
    {
        var result = await mediator.Send(command);
        return Results.Ok(result);
    }

    public static async Task<IResult> DeleteCollectible(int id, IMediator mediator)
    {
        var result = await mediator.Send(new DeleteCollectibleCommand(id));
        return Results.Ok(result);
    }
}
