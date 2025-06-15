using AutoMapper;
using CollectR.Application.Abstractions;
using CollectR.Application.Abstractions.Messaging;
using CollectR.Application.Contracts.Persistence;
using CollectR.Domain;

namespace CollectR.Application.Features.Collections.Commands.CreateCollection;

internal sealed class CreateCollectionCommandHandler(
    ICollectionRepository collectionRepository,
    IMapper mapper
) : ICommandHandler<CreateCollectionCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(
        CreateCollectionCommand request,
        CancellationToken cancellationToken
    )
    {
        var collection = mapper.Map<Collection>(request);

        var result = await collectionRepository.CreateAsync(collection);

        return result.Id;
    }
}
