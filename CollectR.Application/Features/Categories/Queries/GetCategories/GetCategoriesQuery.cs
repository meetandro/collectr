using CollectR.Application.Abstractions.Messaging;

namespace CollectR.Application.Features.Categories.Queries.GetCategories;

public sealed record GetCategoriesQuery : IQuery<IEnumerable<GetCategoriesQueryResponse>>;
