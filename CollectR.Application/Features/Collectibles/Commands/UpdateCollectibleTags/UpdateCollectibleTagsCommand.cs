using CollectR.Application.Abstractions;
using CollectR.Application.Common;

namespace CollectR.Application.Features.Collectibles.Commands.UpdateCollectibleTags;

public sealed record UpdateCollectibleTagsCommand(Guid Id, IEnumerable<Guid> TagIds)
    : ICommand<Result<Unit>>;
