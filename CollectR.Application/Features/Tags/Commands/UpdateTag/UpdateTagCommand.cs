using CollectR.Application.Abstractions;
using CollectR.Application.Common;

namespace CollectR.Application.Features.Tags.Commands.UpdateTag;

public sealed record UpdateTagCommand(Guid Id, string Name, string Hex) : ICommand<Result<Unit>>;
