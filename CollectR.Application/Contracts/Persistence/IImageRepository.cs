using CollectR.Domain;

namespace CollectR.Application.Contracts.Persistence;

public interface IImageRepository : IRepository<Image>
{
    Task<bool> HardDeleteAsync(Guid id);
}
