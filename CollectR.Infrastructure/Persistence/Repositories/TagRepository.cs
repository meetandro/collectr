using CollectR.Application.Contracts.Persistence;
using CollectR.Domain;

namespace CollectR.Infrastructure.Persistence.Repositories;

public class TagRepository(ApplicationDbContext context) : Repository<Tag>(context), ITagRepository;
