using AutoMapper;
using CollectR.Application.Features.Tags.Commands.CreateTag;
using CollectR.Application.Features.Tags.Commands.UpdateTag;
using CollectR.Application.Features.Tags.Queries.GetAllTags;
using CollectR.Application.Features.Tags.Queries.GetTagById;
using CollectR.Domain;

namespace CollectR.Application.Mappings;

public class TagMappings : Profile
{
    public TagMappings()
    {
        CreateMap<CreateTagCommand, Tag>()
            .ReverseMap();

        CreateMap<GetTagByIdQueryResponse, Tag>()
            .ReverseMap();

        CreateMap<UpdateTagCommand, Tag>()
            .ReverseMap();

        CreateMap<UpdateTagCommandResponse, Tag>()
            .ReverseMap();

        CreateMap<GetAllTagsQueryResponse, Tag>()
            .ReverseMap();
    }
}
