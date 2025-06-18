using CollectR.Application.Abstractions;
using CollectR.Application.Common;
using CollectR.Application.Common.Result;

namespace CollectR.Application.Features.Collectibles.Commands.DeleteCollectible;

public sealed record DeleteCollectibleCommand(Guid Id) : ICommand<Result<Unit>>;
