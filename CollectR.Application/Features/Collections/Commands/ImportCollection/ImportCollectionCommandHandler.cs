using CollectR.Application.Abstractions;
using CollectR.Application.Common;
using CollectR.Application.Common.Errors;
using CollectR.Application.Common.Format;
using CollectR.Application.Common.Result;
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
        var extension = Path.GetExtension(request.FileName).ToLowerInvariant();

        var format = FormatHelper.GetFormatFromString(extension);

        if (format == Format.Unknown)
        {
            return FileErrors.UnsupportedFormat(extension);
        }

        var result = format switch
        {
            Format.Excel => await importService.ImportFromExcel(request.Content, cancellationToken),
            Format.Json => await importService.ImportFromJson(request.Content, cancellationToken),
            Format.Xml => await importService.ImportFromXml(request.Content, cancellationToken),
            _ => false
        };

        if (!result)
        {
            return FileErrors.ImportingFailed();
        }

        return Result.Success();
    }
}
