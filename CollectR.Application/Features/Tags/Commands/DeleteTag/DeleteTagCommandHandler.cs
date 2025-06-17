using CollectR.Application.Abstractions;
using CollectR.Application.Common;
using CollectR.Application.Contracts.Persistence;

namespace CollectR.Application.Features.Tags.Commands.DeleteTag;

internal sealed class DeleteTagCommandHandler(ITagRepository tagRepository)
    : ICommandHandler<DeleteTagCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(
        DeleteTagCommand request,
        CancellationToken cancellationToken
    )
    {
        var tag = await tagRepository.GetWithDetailsAsync(request.Id);

        if (tag is null)
        {
            return EntityErrors.NotFound(request.Id);
        }

        await tagRepository.DeleteAsync(request.Id);

        return Result.Success();
    }
}
