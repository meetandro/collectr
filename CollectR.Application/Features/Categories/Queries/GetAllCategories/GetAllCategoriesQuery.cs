using MediatR;

namespace CollectR.Application.Features.Categories.Queries.GetAllCategories;

public sealed record GetAllCategoriesQuery() : IRequest<IEnumerable<GetAllCategoriesQueryResponse>>;
