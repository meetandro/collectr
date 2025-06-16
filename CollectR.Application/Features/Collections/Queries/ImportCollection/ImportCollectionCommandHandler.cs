using CollectR.Application.Abstractions;
using CollectR.Application.Contracts.Persistence;
using CollectR.Application.Abstractions.Messaging;

namespace CollectR.Application.Features.Collections.Queries.ImportCollection;

internal sealed class ImportCollectionCommandHandler(IImportService importService) : ICommandHandler<ImportCollectionCommand, Result>
{
    public async Task<Result> Handle(ImportCollectionCommand request, CancellationToken cancellationToken)
    {
        var extension = Path.GetExtension(request.Format)?.ToLowerInvariant();

        bool result = extension switch
        {
            ".xlsx" => await importService.ImportFromExcel(request.Content, cancellationToken),
            ".json" => await importService.ImportFromJson(request.Content, cancellationToken),
            ".xml" => await importService.ImportFromXml(request.Content, cancellationToken),
            _ => throw new InvalidOperationException($"Unsupported import file format: {extension}")
        };

        return result
            ? Result.Success()
            : Result.Failure(EntityErrors.NotFound(Guid.NewGuid()));
    }
}
