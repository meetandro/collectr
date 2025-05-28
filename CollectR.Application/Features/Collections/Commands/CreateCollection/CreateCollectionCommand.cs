using MediatR;

namespace CollectR.Application.Features.Collections.Commands.CreateCollection;

public sealed record CreateCollectionCommand(string Name, string? Description) : IRequest<int>;
