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

        CreateMap<Tag, GetTagByIdQueryResponse>()
            .ForCtorParam(nameof(GetTagByIdQueryResponse.CollectibleTagIds), opt => opt.MapFrom(src => src.CollectibleTags.Select(ct => ct.CollectibleId)));

        CreateMap<UpdateTagCommand, Tag>()
            .ReverseMap();

        CreateMap<UpdateTagCommandResponse, Tag>()
            .ReverseMap();

        CreateMap<Tag, GetAllTagsQueryResponse>()
            .ForCtorParam(nameof(GetAllTagsQueryResponse.CollectibleTagIds), opt => opt.MapFrom(src => src.CollectibleTags.Select(ct => ct.CollectibleId)));
    }
}
