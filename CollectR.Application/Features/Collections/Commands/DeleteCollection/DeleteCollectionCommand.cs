using CollectR.Application.Abstractions;
using CollectR.Application.Common;

namespace CollectR.Application.Features.Collections.Commands.DeleteCollection;

public sealed record DeleteCollectionCommand(Guid Id) : ICommand<Result<Unit>>;
