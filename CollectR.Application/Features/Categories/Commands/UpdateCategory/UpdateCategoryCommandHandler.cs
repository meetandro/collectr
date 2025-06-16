using AutoMapper;
using CollectR.Application.Abstractions;
using CollectR.Application.Abstractions.Messaging;
using CollectR.Application.Contracts.Persistence;

namespace CollectR.Application.Features.Categories.Commands.UpdateCategory;

internal sealed class UpdateCategoryCommandHandler(
    ICategoryRepository categoryRepository,
    IMapper mapper
) : ICommandHandler<UpdateCategoryCommand, Result>
{
    public async Task<Result> Handle(
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
