using CollectR.Application.Abstractions;
using CollectR.Application.Common;

namespace CollectR.Application.Features.Collections.Queries.GetCollectiblesForCollection;

public sealed record GetCollectiblesForCollectionQuery(
    Guid Id,
    int Page,
    int PageSize,
    string? SearchQuery = null,
    string? Colors = null,
    string? Currency = null,
    decimal? MinValue = null,
    decimal? MaxValue = null,
    string? Conditions = null,
    string? Categories = null,
    string? Tags = null,
    DateTime? AcquiredFrom = null,
    DateTime? AcquiredTo = null,
    bool? IsCollected = null,
    string? SortBy = null,
    string? SortOrder = null
) : IQuery<PaginatedList<GetCollectiblesForCollectionQueryResponse>>;
