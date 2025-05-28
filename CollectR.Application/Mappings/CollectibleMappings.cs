using AutoMapper;
using CollectR.Application.Features.Collectibles.Commands.CreateCollectible;
using CollectR.Application.Features.Collectibles.Commands.UpdateCollectible;
using CollectR.Application.Features.Collectibles.Queries.GetAllCollectibles;
using CollectR.Application.Features.Collectibles.Queries.GetCollectibleById;
using CollectR.Domain;

namespace CollectR.Application.Mappings;

public class CollectibleMappings : Profile // sealed? internal? figure it out.
{
    public CollectibleMappings()
    {
        CreateMap<CreateCollectibleCommand, Collectible>()
            .ForMember(c => c.Images, opt => opt.Ignore());

        CreateMap<Collectible, GetCollectibleByIdQueryResponse>()
            .ForCtorParam(nameof(GetCollectibleByIdQueryResponse.TagIds), opt => opt.MapFrom(src => src.CollectibleTags.Select(ct => ct.TagId)))
            .ForCtorParam(nameof(GetCollectibleByIdQueryResponse.ImageUris), opt => opt.MapFrom(src => src.Images.Select(i => i.Uri)));

        CreateMap<UpdateCollectibleCommand, Collectible>()
            .ReverseMap();

        CreateMap<UpdateCollectibleCommandResponse, Collectible>()
            .ReverseMap();

        CreateMap<Collectible, GetAllCollectiblesQueryResponse>()
            .ForCtorParam(nameof(GetCollectibleByIdQueryResponse.TagIds), opt => opt.MapFrom(src => src.CollectibleTags.Select(ct => ct.TagId)))
            .ForCtorParam(nameof(GetCollectibleByIdQueryResponse.ImageUris), opt => opt.MapFrom(src => src.Images.Select(i => i.Uri)));
    }
}
