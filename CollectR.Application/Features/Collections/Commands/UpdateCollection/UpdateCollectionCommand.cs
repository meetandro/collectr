using MediatR;

namespace CollectR.Application.Features.Collections.Commands.UpdateCollection;

public sealed record UpdateCollectionCommand(int Id, string Name, string? Description)
    : IRequest<UpdateCollectionCommandResponse>;
