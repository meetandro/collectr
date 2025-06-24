using CollectR.Application.Abstractions;
using CollectR.Application.Common.Result;

namespace CollectR.Application.Features.Categories.Queries.GetCategories;

public sealed record GetCategoriesQuery : IQuery<Result<IEnumerable<GetCategoriesQueryResponse>>>;
