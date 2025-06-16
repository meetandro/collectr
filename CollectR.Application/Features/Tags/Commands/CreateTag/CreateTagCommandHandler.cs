using AutoMapper;
using CollectR.Application.Abstractions;
using CollectR.Application.Common;
using CollectR.Application.Contracts.Persistence;
using CollectR.Domain;

namespace CollectR.Application.Features.Tags.Commands.CreateTag;

internal sealed class CreateTagCommandHandler(ITagRepository tagRepository, IMapper mapper)
    : ICommandHandler<CreateTagCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(
        CreateTagCommand request,
        CancellationToken cancellationToken
    )
    {
        var tag = mapper.Map<Tag>(request);

        var result = await tagRepository.CreateAsync(tag);

        return result.Id;
    }
}
