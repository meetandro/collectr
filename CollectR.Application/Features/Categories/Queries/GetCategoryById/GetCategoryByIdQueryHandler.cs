using AutoMapper;
using CollectR.Application.Contracts.Persistence;
using MediatR;

namespace CollectR.Application.Features.Categories.Queries.GetCategoryById;

internal class GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
    : IRequestHandler<GetCategoryByIdQuery, GetCategoryByIdQueryResponse>
{
    public async Task<GetCategoryByIdQueryResponse> Handle(
        GetCategoryByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        var category = await categoryRepository.GetByIdAsync(request.Id);

        var result = mapper.Map<GetCategoryByIdQueryResponse>(category);

        return result;
    }
}
