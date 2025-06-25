using CollectR.Application.Abstractions;
using CollectR.Application.Common;
using CollectR.Application.Common.Errors;
using CollectR.Application.Common.Format;
using CollectR.Application.Common.Result;
using CollectR.Application.Contracts.Services;

namespace CollectR.Application.Features.Collections.Commands.MergeCollection;

internal sealed class MergeCollectionCommandHandler(
    IFileService fileService,
    IImportService importService
) : ICommandHandler<MergeCollectionCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(
        MergeCollectionCommand request,
        CancellationToken cancellationToken
    )
    {
        var extension = Path.GetExtension(request.File.FileName).ToLowerInvariant();

        var format = FormatHelper.GetFormatFromString(extension);

        if (format == Format.Unknown)
        {
            return FileErrors.UnsupportedFormat(extension);
        }

        var content = await fileService.ConvertToByteArrayAsync(request.File);

        var result = format switch
        {
            Format.Excel => await importService.MergeAsync(
                format,
                content,
                request.Id,
                cancellationToken
            ),
            Format.Json => await importService.MergeAsync(
                format,
                content,
                request.Id,
                cancellationToken
            ),
            Format.Xml => await importService.MergeAsync(
                format,
                content,
                request.Id,
                cancellationToken
            ),
            _ => false,
        };

        if (!result)
        {
            return FileErrors.ImportingFailed();
        }

        return Result.Success();
    }
}

