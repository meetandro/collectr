using CollectR.Application.Abstractions;

namespace CollectR.Application.Features.Categories.Queries.GetCategories;

public sealed record GetCategoriesQuery : IQuery<IEnumerable<GetCategoriesQueryResponse>>;
