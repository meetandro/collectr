using CollectR.Application.Abstractions;
using CollectR.Application.Abstractions.Messaging;
using CollectR.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace CollectR.Application.Features.Collectibles.Commands.CreateCollectible;

public sealed record CreateCollectibleCommand(
    string Title,
    string? Description,
    string? Currency,
    decimal? Value,
    DateTime? AcquiredDate,
    bool? IsCollected,
    int? SortIndex,
    Color? Color,
    Condition? Condition,
    Guid AttributesId,
    Guid CategoryId,
    Guid CollectionId,
    IFormFileCollection Images
) : ICommand<Result<Guid>>;
