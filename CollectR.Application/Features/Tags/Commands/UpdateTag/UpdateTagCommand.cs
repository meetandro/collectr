using CollectR.Application.Abstractions;
using CollectR.Application.Common;
using CollectR.Application.Common.Result;

namespace CollectR.Application.Features.Tags.Commands.UpdateTag;

public sealed record UpdateTagCommand(Guid Id, string Name, string Hex) : ICommand<Result<Unit>>;
