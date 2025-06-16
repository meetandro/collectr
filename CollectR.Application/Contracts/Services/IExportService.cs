using CollectR.Application.Features.Collections.Queries.ExportCollection;
using CollectR.Domain;

namespace CollectR.Application.Contracts.Services;

public interface IExportService
{
    Task<byte[]> ExportAsExcel(CollectionDto collection);
    
    Task<byte[]> ExportAsJson(CollectionDto collection);

    Task<byte[]> ExportAsXml(CollectionDto collection);
}
