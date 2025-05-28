namespace CollectR.Application.Features.Categories.Queries.GetCategoryById;

internal sealed record GetCategoryByIdQueryResponse(int Id, string Name, IEnumerable<int> CollectibleIds);
