using AutoMapper;
using CollectR.Application.Contracts.Persistence;
using CollectR.Domain;
using MediatR;

namespace CollectR.Application.Features.Tags.Commands.CreateTag;

internal class CreateTagCommandHandler(ITagRepository tagRepository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateTagCommand, int>
{
    public async Task<int> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        var tag = mapper.Map<Tag>(request);

        var result = await tagRepository.CreateAsync(tag);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return result.Id;
    }
}
