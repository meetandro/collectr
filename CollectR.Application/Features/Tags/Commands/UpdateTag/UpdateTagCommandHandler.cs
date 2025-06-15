using AutoMapper;
using CollectR.Application.Abstractions;
using CollectR.Application.Abstractions.Messaging;
using CollectR.Application.Contracts.Persistence;

namespace CollectR.Application.Features.Tags.Commands.UpdateTag;

internal sealed class UpdateTagCommandHandler(ITagRepository tagRepository, IMapper mapper)
    : ICommandHandler<UpdateTagCommand, Result<UpdateTagCommandResponse>>
{
    public async Task<Result<UpdateTagCommandResponse>> Handle(
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

        var result = mapper.Map<UpdateTagCommandResponse>(tagRepository.Update(tag));

        return result;
    }
}
