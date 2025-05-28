using CollectR.Application.Contracts.Persistence;
using MediatR;

namespace CollectR.Application.Features.Categories.Commands.DeleteCategory;

internal class DeleteCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork) 
    : IRequestHandler<DeleteCategoryCommand, bool>
{
    public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var result = await categoryRepository.DeleteAsync(request.Id);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return result;
    }
}
