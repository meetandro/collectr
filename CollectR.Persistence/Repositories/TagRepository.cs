using CollectR.Application.Contracts.Persistence;
using CollectR.Domain;

namespace CollectR.Persistence.Repositories;

public sealed class TagRepository(IApplicationDbContext context)
    : Repository<Tag>(context),
        ITagRepository;
