using MediatR;

namespace CollectR.Application.Features.Tags.Commands.UpdateTag;

public sealed record UpdateTagCommand(int Id, string Name, string Hex, int CollectionId) : IRequest<UpdateTagCommandResponse>;
