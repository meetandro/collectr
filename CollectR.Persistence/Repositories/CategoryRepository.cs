using CollectR.Application.Contracts.Persistence;
using CollectR.Domain;

namespace CollectR.Persistence.Repositories;

public sealed class CategoryRepository(IApplicationDbContext context)
    : Repository<Category>(context),
        ICategoryRepository;
