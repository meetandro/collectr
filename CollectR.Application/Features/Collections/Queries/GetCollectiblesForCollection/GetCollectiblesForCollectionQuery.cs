using CollectR.Application.Abstractions.Messaging;
using CollectR.Application.Models;

namespace CollectR.Application.Features.Collections.Queries.GetCollectiblesForCollection;

public sealed record GetCollectiblesForCollectionQuery(
    Guid Id,
    int Page,
    int PageSize,
    string? Query = null,
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
