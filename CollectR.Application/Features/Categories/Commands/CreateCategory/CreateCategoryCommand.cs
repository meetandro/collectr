using CollectR.Application.Abstractions;
using CollectR.Application.Common.Result;

namespace CollectR.Application.Features.Categories.Commands.CreateCategory;

public sealed record CreateCategoryCommand(string Name) : ICommand<Result<Guid>>;
