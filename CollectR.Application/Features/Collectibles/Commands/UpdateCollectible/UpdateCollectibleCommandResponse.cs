using CollectR.Domain.Enums;

namespace CollectR.Application.Features.Collectibles.Commands.UpdateCollectible;

internal sealed record UpdateCollectibleCommandResponse(
    string Title,
    string? Description,
    string? Currency,
    decimal? Value,
    DateTime? AcquiredDate,
    bool? IsCollected,
    int? SortIndex,
    Color? Color,
    Condition? Condition
);
