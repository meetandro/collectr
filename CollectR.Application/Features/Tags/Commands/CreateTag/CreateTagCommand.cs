using MediatR;

namespace CollectR.Application.Features.Tags.Commands.CreateTag;

public sealed record CreateTagCommand(string Name, string Hex, int CollectionId) : IRequest<int>;
