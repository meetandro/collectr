using AutoMapper;
using CollectR.Application.Contracts.Persistence;
using CollectR.Domain;
using MediatR;

namespace CollectR.Application.Features.Collections.Commands.CreateCollection;

internal class CreateCollectionCommandHandler(ICollectionRepository collectionRepository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateCollectionCommand, int>
{
    public async Task<int> Handle(CreateCollectionCommand request, CancellationToken cancellationToken)
    {
        var collection = mapper.Map<Collection>(request);

        var result = await collectionRepository.CreateAsync(collection);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return result.Id;
    }
}
