using CollectR.Domain;

namespace CollectR.Application.Contracts.Persistence;

public interface IImageRepository
{
    Task<IEnumerable<Image>> CreateRangeAsync(IEnumerable<Image> images);

    Task<bool> DeleteAsync(int id);
}
