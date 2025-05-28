using AutoMapper;
using CollectR.Application.Contracts.Persistence;
using MediatR;

namespace CollectR.Application.Features.Tags.Commands.UpdateTag;

internal class UpdateTagCommandHandler(ITagRepository tagRepository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateTagCommand, UpdateTagCommandResponse>
{
    public async Task<UpdateTagCommandResponse> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
    {
        var tag = await tagRepository.GetByIdAsync(request.Id)
            ?? throw new NotImplementedException();

        mapper.Map(request, tag);

        var result = tagRepository.Update(tag);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<UpdateTagCommandResponse>(result);
    }
}
