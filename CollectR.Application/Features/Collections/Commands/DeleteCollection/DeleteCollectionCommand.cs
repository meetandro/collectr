using CollectR.Application.Abstractions;
using CollectR.Application.Abstractions.Messaging;

namespace CollectR.Application.Features.Collections.Commands.DeleteCollection;

public sealed record DeleteCollectionCommand(Guid Id) : ICommand<Result>;
