using AutoMapper;
using CollectR.Application.Contracts.Persistence;
using CollectR.Domain;
using MediatR;

namespace CollectR.Application.Features.Categories.Commands.CreateCategory;

internal class CreateCategoryCommandHandler(
    ICategoryRepository categoryRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateCategoryCommand, int> // fix order, unit of work second
{
    public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = mapper.Map<Category>(request);

        var result = await categoryRepository.CreateAsync(category);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return result.Id;
    }
}
