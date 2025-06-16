using AutoMapper;
using CollectR.Application.Abstractions;
using CollectR.Application.Common;
using CollectR.Application.Contracts.Persistence;

namespace CollectR.Application.Features.Categories.Commands.UpdateCategory;

internal sealed class UpdateCategoryCommandHandler(
    ICategoryRepository categoryRepository,
    IMapper mapper
) : ICommandHandler<UpdateCategoryCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(
        UpdateCategoryCommand request,
        CancellationToken cancellationToken
    )
    {
        var category = await categoryRepository.GetByIdAsync(request.Id);

        if (category is null)
        {
            return EntityErrors.NotFound(request.Id);
        }

        mapper.Map(request, category);

        categoryRepository.Update(category);

        return Result.Success();
    }
}
