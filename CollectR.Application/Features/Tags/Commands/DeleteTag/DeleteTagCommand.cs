using CollectR.Application.Abstractions;
using CollectR.Application.Common;

namespace CollectR.Application.Features.Tags.Commands.DeleteTag;

public sealed record DeleteTagCommand(Guid Id) : ICommand<Result<Unit>>;
