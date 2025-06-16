using AutoMapper;
using CollectR.Application.Abstractions;
using CollectR.Application.Abstractions.Messaging;
using CollectR.Application.Contracts.Persistence;

namespace CollectR.Application.Features.Tags.Commands.UpdateTag;

internal sealed class UpdateTagCommandHandler(ITagRepository tagRepository, IMapper mapper)
    : ICommandHandler<UpdateTagCommand, Result>
{
    public async Task<Result> Handle(
        UpdateTagCommand request,
        CancellationToken cancellationToken
    )
    {
        var tag = await tagRepository.GetByIdAsync(request.Id);

        if (tag is null)
        {
            return EntityErrors.NotFound(request.Id);
        }

        mapper.Map(request, tag);

        tagRepository.Update(tag);

        return Result.Success();
    }
}
