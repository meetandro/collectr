using CollectR.Application.Abstractions;
using CollectR.Application.Common;
using CollectR.Application.Common.Result;
using Microsoft.AspNetCore.Http;

namespace CollectR.Application.Features.Collections.Commands.ImportCollection;

public sealed record ImportCollectionCommand(IFormFile File)
    : ICommand<Result<Unit>>;
