using CollectR.Application.Abstractions;
using CollectR.Application.Common;

namespace CollectR.Application.Features.Categories.Commands.CreateCategory;

public sealed record CreateCategoryCommand(string Name) : ICommand<Result<Guid>>;
