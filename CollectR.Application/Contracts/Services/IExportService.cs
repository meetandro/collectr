using CollectR.Application.Common.Format;
using CollectR.Application.Contracts.Models;

namespace CollectR.Application.Contracts.Services;

public interface IExportService
{
    byte[] Export(Format format, CollectionDto collection);
}
