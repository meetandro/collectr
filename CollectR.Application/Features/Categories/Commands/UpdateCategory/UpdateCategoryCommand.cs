using MediatR;

namespace CollectR.Application.Features.Categories.Commands.UpdateCategory;

public sealed record UpdateCategoryCommand(int Id, string Name) : IRequest<UpdateCategoryCommandResponse>;
