using CollectR.Application.Abstractions;
using CollectR.Application.Abstractions.Messaging;

namespace CollectR.Application.Features.Collections.Queries.ImportCollection;

public sealed record ImportCollectionCommand(byte[] Content, string Format) : ICommand<Result>;
