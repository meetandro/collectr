using CollectR.Application.Contracts.Persistence;
using MediatR;

namespace CollectR.Application.Features.Collections.Commands.DeleteCollection;

internal class DeleteCollectionCommandHandler(ICollectionRepository collectionRepository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteCollectionCommand, bool>
{
    public async Task<bool> Handle(DeleteCollectionCommand request, CancellationToken cancellationToken)
    {
        var result = await collectionRepository.DeleteAsync(request.Id);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return result;
    }
}
