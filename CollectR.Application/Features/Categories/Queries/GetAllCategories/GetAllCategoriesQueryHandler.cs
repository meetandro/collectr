using AutoMapper;
using CollectR.Application.Contracts.Persistence;
using MediatR;

namespace CollectR.Application.Features.Categories.Queries.GetAllCategories;

internal class GetAllCategoriesQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
    : IRequestHandler<GetAllCategoriesQuery, IEnumerable<GetAllCategoriesQueryResponse>>
{
    public async Task<IEnumerable<GetAllCategoriesQueryResponse>> Handle(
        GetAllCategoriesQuery request,
        CancellationToken cancellationToken
    )
    {
        var result = await categoryRepository.GetAllAsync(); // collectibleids

        return result.Select(mapper.Map<GetAllCategoriesQueryResponse>);
    }
}
