using CollectR.Application.Abstractions;
using CollectR.Application.Abstractions.Messaging;

namespace CollectR.Application.Features.Categories.Commands.DeleteCategory;

public sealed record DeleteCategoryCommand(Guid Id) : ICommand<Result>;
