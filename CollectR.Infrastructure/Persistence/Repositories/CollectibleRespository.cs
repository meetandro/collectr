using CollectR.Application.Contracts.Persistence;
using CollectR.Domain;

namespace CollectR.Infrastructure.Persistence.Repositories;

public class CollectibleRespository(ApplicationDbContext context) : Repository<Collectible>(context), ICollectibleRepository;
