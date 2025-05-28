using MediatR;

namespace CollectR.Application.Features.Collections.Commands.DeleteCollection;

public sealed record DeleteCollectionCommand(int Id) : IRequest<bool>;
