using CollectR.Application.Abstractions;
using CollectR.Application.Common;
using CollectR.Application.Common.Result;
using Microsoft.AspNetCore.Http;

namespace CollectR.Application.Features.Collections.Commands.MergeCollection;

public sealed record MergeCollectionCommand(Guid Id, IFormFile File) : ICommand<Result<Unit>>;
