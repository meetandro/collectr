using CollectR.Application.Contracts.Persistence;
using CollectR.Domain;

namespace CollectR.Persistence.Repositories;

public sealed class CollectionRepository(IApplicationDbContext context)
    : Repository<Collection>(context),
        ICollectionRepository;
