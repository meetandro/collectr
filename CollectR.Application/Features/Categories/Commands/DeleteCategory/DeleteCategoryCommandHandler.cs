using CollectR.Application.Abstractions;
using CollectR.Application.Abstractions.Messaging;
using CollectR.Application.Contracts.Persistence;

namespace CollectR.Application.Features.Categories.Commands.DeleteCategory;

internal sealed class DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
    : ICommandHandler<DeleteCategoryCommand, Result>
{
    public async Task<Result> Handle(
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
