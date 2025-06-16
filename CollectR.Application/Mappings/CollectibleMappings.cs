using AutoMapper;
using CollectR.Application.Features.Collectibles.Commands.CreateCollectible;
using CollectR.Application.Features.Collectibles.Commands.UpdateCollectible;
using CollectR.Application.Features.Collectibles.Queries.GetCollectibleById;
using CollectR.Application.Features.Collectibles.Queries.GetCollectibles;
using CollectR.Application.Features.Collections.Queries.GetCollectiblesForCollection;
using CollectR.Domain;

namespace CollectR.Application.Mappings;

public class CollectibleMappings : Profile // sealed? internal? figure it out.
{
    public CollectibleMappings()
    {
        CreateMap<string, Attributes>()
            .ConvertUsing(src => new Attributes { Metadata = src });

        CreateMap<Attributes, string>()
            .ConvertUsing(src => src.Metadata);

        CreateMap<CreateCollectibleCommand, Collectible>() // metadata
            .ForMember(c => c.Attributes, dest => dest.MapFrom(c => c.Metadata))
            .ForMember(c => c.Images, opt => opt.Ignore()); // check extra reversemaps in other profiles

        CreateMap<Collectible, GetCollectibleByIdQueryResponse>()
            .ForCtorParam(
                nameof(GetCollectibleByIdQueryResponse.TagIds),
                opt => opt.MapFrom(src => src.CollectibleTags.Select(ct => ct.TagId))
            )
            .ForCtorParam(
                nameof(GetCollectibleByIdQueryResponse.ImageUris),
                opt => opt.MapFrom(src => src.Images.Select(i => i.Uri))
            )
            .ForCtorParam(nameof(GetCollectibleByIdQueryResponse.Metadata), dest => dest.MapFrom(c => c.Attributes));

        CreateMap<Collectible, GetCollectiblesForCollectionQueryResponse>() // collection mapping in collectible mappings?
            .ForCtorParam(
                nameof(GetCollectiblesForCollectionQueryResponse.TagIds),
                opt => opt.MapFrom(src => src.CollectibleTags.Select(ct => ct.TagId))
            )
            .ForCtorParam(
                nameof(GetCollectiblesForCollectionQueryResponse.ImageUris),
                opt => opt.MapFrom(src => src.Images.Select(i => i.Uri))
            )
            .ForCtorParam(nameof(GetCollectibleByIdQueryResponse.Metadata), dest => dest.MapFrom(c => c.Attributes));

        CreateMap<UpdateCollectibleCommand, Collectible>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember is not null));

        CreateMap<Collectible, GetCollectiblesQueryResponse>()
            .ForCtorParam(
                nameof(GetCollectiblesQueryResponse.TagIds),
                opt => opt.MapFrom(src => src.CollectibleTags.Select(ct => ct.TagId))
            )
            .ForCtorParam(
                nameof(GetCollectiblesQueryResponse.ImageUris),
                opt => opt.MapFrom(src => src.Images.Select(i => i.Uri))
            )
            .ForCtorParam(nameof(GetCollectibleByIdQueryResponse.Metadata), dest => dest.MapFrom(c => c.Attributes));
    }
}
