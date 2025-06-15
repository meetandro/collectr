using CollectR.Application.Contracts.Persistence;
using CollectR.Domain;

namespace CollectR.Persistence.Repositories;

public class ImageRepository(IApplicationDbContext context)
    : Repository<Image>(context),
        IImageRepository;
