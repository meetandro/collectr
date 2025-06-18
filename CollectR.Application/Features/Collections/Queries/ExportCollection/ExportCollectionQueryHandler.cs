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
    IApplicationDbContext context
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
            .Select(c => new CollectionDto
            {
                Name = c.Name,
                Description = c.Description,
                Collectibles = c
                    .Collectibles.Select(col => new CollectibleDto
                    {
                        Title = col.Title,
                        Description = col.Description,
                        Currency = col.Currency,
                        Value = col.Value,
                        AcquiredDate = col.AcquiredDate,
                        IsCollected = col.IsCollected,
                        SortIndex = col.SortIndex,
                        Color = col.Color,
                        Condition = col.Condition,
                        Metadata = col.Attributes != null ? col.Attributes.Metadata : "{}",
                        Category = col.Category != null ? col.Category.Name : string.Empty,
                        Tags = col
                            .CollectibleTags.Select(ct => new TagDto
                            {
                                Name = ct.Tag.Name,
                                Hex = ct.Tag.Hex,
                            })
                            .ToList(),
                    })
                    .ToList(),
            })
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
