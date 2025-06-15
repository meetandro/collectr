namespace CollectR.Application.Features.Categories.Queries.GetCategoryById;

internal sealed record GetCategoryByIdQueryResponse(
    Guid Id,
    string Name,
    IEnumerable<Guid> CollectibleIds
);
