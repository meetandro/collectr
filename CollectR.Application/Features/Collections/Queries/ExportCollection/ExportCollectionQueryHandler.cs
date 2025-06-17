using CollectR.Application.Abstractions;
using CollectR.Application.Common;
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
        var collection = await context
            .Collections.Where(c => c.Id == request.Id)
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

        var res = request.Format.ToLower() switch
        {
            "json" => (
                await exportService.ExportAsJson(collection),
                "application/json",
                $"Collection-{collection.Name}.json"
            ),
            "xml" => (
                await exportService.ExportAsXml(collection),
                "application/xml",
                $"Collection-{collection.Name}.xml"
            ),
            "excel" => (
                await exportService.ExportAsExcel(collection),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"Collection-{collection.Name}.xlsx"
            ),
            _ => throw new InvalidOperationException(
                "Unsupported format."
            ) // return an error instead of these
            ,
        };

        var result = new ExportCollectionQueryResponse(res.Item1, res.Item2, res.Item3);

        return result;
    }
}
