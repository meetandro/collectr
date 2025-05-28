using AutoMapper;
using CollectR.Application.Contracts.Persistence;
using MediatR;

namespace CollectR.Application.Features.Categories.Commands.UpdateCategory;

internal class UpdateCategoryCommandHandler(
    ICategoryRepository categoryRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork
) : IRequestHandler<UpdateCategoryCommand, UpdateCategoryCommandResponse>
{
    public async Task<UpdateCategoryCommandResponse> Handle(
        UpdateCategoryCommand request,
        CancellationToken cancellationToken
    )
    {
        var category = await categoryRepository.GetByIdAsync(request.Id)
            ?? throw new ArgumentNullException(nameof(request));

        mapper.Map(request, category);

        var result = categoryRepository.Update(category);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<UpdateCategoryCommandResponse>(result);
    }
}
