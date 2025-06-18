using CollectR.Application.Abstractions;
using CollectR.Application.Common.Result;

namespace CollectR.Application.Features.Collections.Commands.CreateCollection;

public sealed record CreateCollectionCommand(string Name, string? Description)
    : ICommand<Result<Guid>>;
