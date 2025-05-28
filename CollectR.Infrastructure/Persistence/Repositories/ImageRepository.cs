using CollectR.Application.Contracts.Persistence;
using CollectR.Domain;

namespace CollectR.Infrastructure.Persistence.Repositories;

public class ImageRepository(ApplicationDbContext context) : Repository<Image>(context), IImageRepository;
