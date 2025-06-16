using CollectR.Application.Abstractions;
using CollectR.Application.Common;
using CollectR.Application.Contracts.Persistence;

namespace CollectR.Application.Features.Categories.Commands.DeleteCategory;

internal sealed class DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
    : ICommandHandler<DeleteCategoryCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(
        DeleteCategoryCommand request,
        CancellationToken cancellationToken
    )
    {
        var category = await categoryRepository.GetByIdAsync(request.Id);

        if (category is null)
        {
            return EntityErrors.NotFound(request.Id);
        }

        await categoryRepository.DeleteAsync(request.Id);

        return Result.Success();
    }
}
