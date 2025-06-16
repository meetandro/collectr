namespace CollectR.Application.Contracts.Services;

public interface IImportService
{
    Task<bool> ImportFromExcel(byte[] content, CancellationToken cancellationToken);

    Task<bool> ImportFromJson(byte[] content, CancellationToken cancellationToken);

    Task<bool> ImportFromXml(byte[] content, CancellationToken cancellationToken);
}
