using CollectR.Application.Abstractions;
using CollectR.Application.Common;
using CollectR.Application.Common.Result;
using CollectR.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace CollectR.Application.Features.Collectibles.Commands.UpdateCollectible;

public sealed record UpdateCollectibleCommand(
    Guid Id,
    string? Title,
    string? Description,
    string? Currency,
    decimal? Value,
    DateTime? AcquiredDate,
    bool? IsCollected,
    int? SortIndex,
    Color? Color,
    Condition? Condition,
    string? Metadata,
    Guid CategoryId,
    string? ExistingImageUris,
    IFormFileCollection? NewImages
) : ICommand<Result<Unit>>;
