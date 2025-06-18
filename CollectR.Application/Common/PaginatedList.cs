namespace CollectR.Application.Common;

public sealed record PaginatedList<T>(IEnumerable<T> Items, int TotalCount, int Page, int PageSize)
    where T : class;
