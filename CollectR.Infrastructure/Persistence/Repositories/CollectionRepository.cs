using CollectR.Application.Contracts.Persistence;
using CollectR.Domain;

namespace CollectR.Infrastructure.Persistence.Repositories;

public class CollectionRepository(ApplicationDbContext context)
    : Repository<Collection>(context), ICollectionRepository;
