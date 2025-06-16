using CollectR.Application.Abstractions;
using CollectR.Application.Abstractions.Messaging;

namespace CollectR.Application.Features.Tags.Commands.UpdateTag;

public sealed record UpdateTagCommand(Guid Id, string Name, string Hex) : ICommand<Result>;
