using CollectR.Application.Abstractions;
using CollectR.Application.Abstractions.Messaging;

namespace CollectR.Application.Features.Tags.Commands.DeleteTag;

public sealed record DeleteTagCommand(Guid Id) : ICommand<Result>;
