using AutoMapper;
using AutoMapper.QueryableExtensions;
using CollectR.Application.Abstractions;
using CollectR.Application.Common.Errors;
using CollectR.Application.Common.Format;
using CollectR.Application.Common.Result;
using CollectR.Application.Contracts.Models;
using CollectR.Application.Contracts.Persistence;
using CollectR.Application.Contracts.Services;
using Microsoft.EntityFrameworkCore;

namespace CollectR.Application.Features.Collections.Queries.ExportCollection;

internal sealed class ExportCollectionQueryHandler(
    IExportService exportService,
    IApplicationDbContext context,
    IMapper mapper
) : IQueryHandler<ExportCollectionQuery, Result<ExportCollectionQueryResponse>>
{
    public async Task<Result<ExportCollectionQueryResponse>> Handle(
        ExportCollectionQuery request,
        CancellationToken cancellationToken
    )
    {
        var collection = await context.Collections
            .Where(c => c.Id == request.Id)
            .AsNoTracking()
            .AsSplitQuery()
            .ProjectTo<CollectionDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (collection is null)
        {
            return EntityErrors.NotFound(request.Id);
        }

        var format = FormatHelper.GetFormatFromString(request.Format);

        if (format == Format.Unknown)
        {
            return FileErrors.UnsupportedFormat(request.Format);
        }

        (byte[] fileContents, string contentType, string fileName) = format switch
        {
            Format.Excel => (
                await exportService.ExportAsExcel(collection),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"Collection-{collection.Name}.xlsx"
            ),
            Format.Json => (
                await exportService.ExportAsJson(collection),
                "application/json",
                $"Collection-{collection.Name}.json"
            ),
            Format.Xml => (
                await exportService.ExportAsXml(collection),
                "application/xml",
                $"Collection-{collection.Name}.xml"
            ),
            _ => throw new InvalidOperationException("Unexpected format"),
        };

        var result = new ExportCollectionQueryResponse(fileContents, contentType, fileName);

        return result;
    }
}
