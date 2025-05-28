using CollectR.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CollectR.Application.Features.Collectibles.Commands.CreateCollectible;

public sealed record CreateCollectibleCommand(
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
    IFormFileCollection Images
) : IRequest<int>;
