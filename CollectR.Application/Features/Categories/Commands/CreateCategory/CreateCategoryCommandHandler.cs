using AutoMapper;
using CollectR.Application.Abstractions;
using CollectR.Application.Common.Result;
using CollectR.Application.Contracts.Persistence;
using CollectR.Domain;

namespace CollectR.Application.Features.Categories.Commands.CreateCategory;

internal sealed class CreateCategoryCommandHandler(
    ICategoryRepository categoryRepository,
    IMapper mapper
) : ICommandHandler<CreateCategoryCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(
        CreateCategoryCommand request,
        CancellationToken cancellationToken
    )
    {
        var category = mapper.Map<Category>(request);

        var result = await categoryRepository.CreateAsync(category);

        return result.Id;
    }
}
