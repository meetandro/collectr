using CollectR.Application.Abstractions;
using CollectR.Application.Abstractions.Messaging;

namespace CollectR.Application.Features.Categories.Commands.UpdateCategory;

public sealed record UpdateCategoryCommand(Guid Id, string Name) : ICommand<Result>;
