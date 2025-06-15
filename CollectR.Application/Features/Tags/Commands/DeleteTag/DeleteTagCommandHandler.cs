using CollectR.Application.Abstractions;
using CollectR.Application.Abstractions.Messaging;
using CollectR.Application.Contracts.Persistence;

namespace CollectR.Application.Features.Tags.Commands.DeleteTag;

internal sealed class DeleteTagCommandHandler(ITagRepository tagRepository)
    : ICommandHandler<DeleteTagCommand, Result>
{
    public async Task<Result> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
    {
        var tag = await tagRepository.GetByIdAsync(request.Id);

        if (tag is null)
        {
            return EntityErrors.NotFound(request.Id);
        }

        await tagRepository.DeleteAsync(request.Id);

        return Result.Success();
    }
}
