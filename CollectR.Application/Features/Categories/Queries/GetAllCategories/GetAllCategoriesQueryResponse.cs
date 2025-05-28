namespace CollectR.Application.Features.Categories.Queries.GetAllCategories;

internal sealed record GetAllCategoriesQueryResponse(int Id, string Name, IEnumerable<int> CollectibleIds);
