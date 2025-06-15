using CollectR.Application.Contracts.Persistence;
using CollectR.Domain;

namespace CollectR.Persistence.Repositories;

public class CollectibleRespository(IApplicationDbContext context)
    : Repository<Collectible>(context),
        ICollectibleRepository;
