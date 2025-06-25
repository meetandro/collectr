using CollectR.Application.Common.Format;

namespace CollectR.Application.Contracts.Services;

public interface IImportService
{
    Task<bool> ImportAsync(Format format, byte[] content, CancellationToken cancellationToken);

    Task<bool> MergeAsync(Format format, byte[] content, Guid collectionId, CancellationToken cancellationToken);
}
