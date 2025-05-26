using MediatR;

namespace CollectR.Application.Features.Categories.Commands.CreateCategory;

public sealed record CreateCategoryCommand(string Name) : IRequest<int>;
