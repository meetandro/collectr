namespace CollectR.Application.Features.Categories.Queries.GetCategories;

public sealed record GetCategoriesQueryResponse(
    Guid Id,
    string Name,
    IEnumerable<Guid> CollectibleIds
);
