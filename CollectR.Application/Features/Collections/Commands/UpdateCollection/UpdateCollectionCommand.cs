using CollectR.Application.Abstractions;
using CollectR.Application.Abstractions.Messaging;

namespace CollectR.Application.Features.Collections.Commands.UpdateCollection;

public sealed record UpdateCollectionCommand(Guid Id, string Name, string? Description) : ICommand<Result>;
