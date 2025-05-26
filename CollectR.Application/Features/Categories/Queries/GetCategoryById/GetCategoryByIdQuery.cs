using MediatR;

namespace CollectR.Application.Features.Categories.Queries.GetCategoryById;

public sealed record GetCategoryByIdQuery(int Id) : IRequest<GetCategoryByIdQueryResponse>;
