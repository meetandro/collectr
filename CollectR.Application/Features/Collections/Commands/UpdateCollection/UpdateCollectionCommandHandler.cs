using AutoMapper;
using CollectR.Application.Contracts.Persistence;
using CollectR.Domain;
using MediatR;

namespace CollectR.Application.Features.Collections.Commands.UpdateCollection;

internal class UpdateCollectionCommandHandler(ICollectionRepository collectionRepository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateCollectionCommand, UpdateCollectionCommandResponse>
{
    public async Task<UpdateCollectionCommandResponse> Handle(UpdateCollectionCommand request, CancellationToken cancellationToken)
    {
        var collection = await collectionRepository.GetByIdAsync(request.Id)
            ?? throw new NotImplementedException();

        mapper.Map(request, collection);

        var result = collectionRepository.Update(collection);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<UpdateCollectionCommandResponse>(result);
    }
}
