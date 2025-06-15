namespace CollectR.Application.Features.Categories.Queries.GetCategories;

internal sealed record GetCategoriesQueryResponse(
    Guid Id,
    string Name,
    IEnumerable<Guid> CollectibleIds
);
