using CollectR.Application.Abstractions;
using CollectR.Application.Common;
using CollectR.Application.Common.Errors;
using CollectR.Application.Common.Format;
using CollectR.Application.Common.Result;
using CollectR.Application.Contracts.Services;

namespace CollectR.Application.Features.Collections.Commands.ImportCollection;

internal sealed class ImportCollectionCommandHandler(
    IFileService fileService,
    IImportService importService
) : ICommandHandler<ImportCollectionCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(
        ImportCollectionCommand request,
        CancellationToken cancellationToken
    )
    {
        var extension = Path.GetExtension(request.File.FileName);

        var format = FormatHelper.GetFormatFromString(extension);

        if (format == Format.Unknown)
        {
            return FileErrors.UnsupportedFormat(extension);
        }

        var content = await fileService.ConvertToByteArrayAsync(request.File);
        
        var result = await importService.ImportAsync(format, content, cancellationToken);

        if (!result)
        {
            return FileErrors.ImportingFailed();
        }

        return Result.Success();
    }
}
