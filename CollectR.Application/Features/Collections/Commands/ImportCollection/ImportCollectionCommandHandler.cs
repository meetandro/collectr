using CollectR.Application.Abstractions;
using CollectR.Application.Common;
using CollectR.Application.Contracts.Services;

namespace CollectR.Application.Features.Collections.Commands.ImportCollection;

internal sealed class ImportCollectionCommandHandler(IImportService importService)
    : ICommandHandler<ImportCollectionCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(
        ImportCollectionCommand request,
        CancellationToken cancellationToken
    )
    {
        var extension = Path.GetExtension(request.FileName)?.ToLowerInvariant();

        bool result = extension switch
        {
            ".xlsx" => await importService.ImportFromExcel(request.Content, cancellationToken),
            ".json" => await importService.ImportFromJson(request.Content, cancellationToken),
            ".xml" => await importService.ImportFromXml(request.Content, cancellationToken),
            _ => throw new InvalidOperationException(
                $"Unsupported import file format: {extension}"
            ),
        };

        return result ? Result.Success() : EntityErrors.NotFound(Guid.NewGuid()); // fix this
    }
}
