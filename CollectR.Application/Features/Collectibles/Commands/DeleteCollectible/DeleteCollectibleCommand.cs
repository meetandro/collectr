using CollectR.Application.Abstractions;
using CollectR.Application.Abstractions.Messaging;

namespace CollectR.Application.Features.Collectibles.Commands.DeleteCollectible;

public sealed record DeleteCollectibleCommand(Guid Id) : ICommand<Result>;
