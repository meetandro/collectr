using MediatR;

namespace CollectR.Application.Features.Tags.Commands.DeleteTag;

public sealed record DeleteTagCommand(int Id) : IRequest<bool>;
