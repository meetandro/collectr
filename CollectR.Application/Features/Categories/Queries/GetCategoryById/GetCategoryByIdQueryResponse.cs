namespace CollectR.Application.Features.Categories.Queries.GetCategoryById;

public sealed record GetCategoryByIdQueryResponse(
    Guid Id,
    string Name,
    IEnumerable<Guid> CollectibleIds
);
