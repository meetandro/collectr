using CollectR.Application.Abstractions;
using CollectR.Application.Common;
using CollectR.Application.Common.Result;

namespace CollectR.Application.Features.Collections.Commands.DeleteCollection;

public sealed record DeleteCollectionCommand(Guid Id) : ICommand<Result<Unit>>;
