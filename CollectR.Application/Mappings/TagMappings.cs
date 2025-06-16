using AutoMapper;
using CollectR.Application.Features.Collections.Queries.GetTagsForCollection;
using CollectR.Application.Features.Tags.Commands.CreateTag;
using CollectR.Application.Features.Tags.Commands.UpdateTag;
using CollectR.Application.Features.Tags.Queries.GetTagById;
using CollectR.Application.Features.Tags.Queries.GetTags;
using CollectR.Domain;

namespace CollectR.Application.Mappings;

public class TagMappings : Profile
{
    public TagMappings()
    {
        CreateMap<CreateTagCommand, Tag>().ReverseMap();

        CreateMap<Tag, GetTagByIdQueryResponse>()
            .ForCtorParam(
                nameof(GetTagByIdQueryResponse.CollectibleIds),
                opt => opt.MapFrom(src => src.CollectibleTags.Select(ct => ct.CollectibleId))
            );

        CreateMap<Tag, GetTagsForCollectionQueryResponse>()
            .ForCtorParam(
                nameof(GetTagByIdQueryResponse.CollectibleIds),
                opt => opt.MapFrom(src => src.CollectibleTags.Select(ct => ct.CollectibleId))
            );

        CreateMap<UpdateTagCommand, Tag>().ReverseMap();

        CreateMap<Tag, GetTagsQueryResponse>()
            .ForCtorParam(
                nameof(GetTagsQueryResponse.CollectibleIds),
                opt => opt.MapFrom(src => src.CollectibleTags.Select(ct => ct.CollectibleId))
            );
    }
}
