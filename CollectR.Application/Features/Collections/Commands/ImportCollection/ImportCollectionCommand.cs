using CollectR.Application.Abstractions;
using CollectR.Application.Common;
using CollectR.Application.Common.Result;

namespace CollectR.Application.Features.Collections.Commands.ImportCollection;

public sealed record ImportCollectionCommand(byte[] Content, string FileName)
    : ICommand<Result<Unit>>;
