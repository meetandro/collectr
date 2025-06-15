using CollectR.Application.Abstractions;
using CollectR.Application.Abstractions.Messaging;
using CollectR.Domain.Enums;

namespace CollectR.Application.Features.Collectibles.Commands.UpdateCollectible;

public sealed record UpdateCollectibleCommand(
    Guid Id,
    string Title,
    string? Description,
    string? Currency,
    decimal? Value,
    DateTime? AcquiredDate,
    bool? IsCollected,
    int? SortIndex,
    Color? Color,
    Condition? Condition
) : ICommand<Result<UpdateCollectibleCommandResponse>>;
