using CollectR.Application.Contracts.Persistence;
using MediatR;

namespace CollectR.Application.Features.Tags.Commands.DeleteTag;

internal class DeleteTagCommandHandler(ITagRepository tagRepository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteTagCommand, bool>
{
    public async Task<bool> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
    {
        var result = await tagRepository.DeleteAsync(request.Id);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return result;
    }
}
