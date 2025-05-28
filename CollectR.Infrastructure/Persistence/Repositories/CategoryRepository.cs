using CollectR.Application.Contracts.Persistence;
using CollectR.Domain;

namespace CollectR.Infrastructure.Persistence.Repositories;

public class CategoryRepository(ApplicationDbContext context)
    : Repository<Category>(context), ICategoryRepository;
