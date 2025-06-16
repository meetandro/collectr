using CollectR.Application.Abstractions;

namespace CollectR.Application.Contracts.Persistence;

public interface IImportService
{
    Task<bool> ImportFromJson(byte[] content, CancellationToken cancellationToken);

    Task<bool> ImportFromXml(byte[] content, CancellationToken cancellationToken);

    Task<bool> ImportFromExcel(byte[] content, CancellationToken cancellationToken);
}
