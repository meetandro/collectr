using CollectR.Application.Abstractions;
using CollectR.Application.Common;
using CollectR.Application.Common.Result;

namespace CollectR.Application.Features.Collectibles.Commands.UpdateCollectibleTags;

public sealed record UpdateCollectibleTagsCommand(Guid Id, IEnumerable<Guid> TagIds)
    : ICommand<Result<Unit>>;
