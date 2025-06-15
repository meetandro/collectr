using CollectR.Application.Abstractions;
using CollectR.Application.Abstractions.Messaging;

namespace CollectR.Application.Features.Tags.Commands.CreateTag;

public sealed record CreateTagCommand(string Name, string Hex, Guid CollectionId)
    : ICommand<Result<Guid>>;
