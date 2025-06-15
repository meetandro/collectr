using CollectR.Application.Contracts.Persistence;
using CollectR.Domain;

namespace CollectR.Persistence.Repositories;

public class TagRepository(IApplicationDbContext context)
    : Repository<Tag>(context),
        ITagRepository;
