using AutoMapper;
using CollectR.Application.Features.Collections.Commands.CreateCollection;
using CollectR.Application.Features.Collections.Commands.UpdateCollection;
using CollectR.Application.Features.Collections.Queries.GetCollectionById;
using CollectR.Application.Features.Collections.Queries.GetCollections;
using CollectR.Domain;

namespace CollectR.Application.Mappings;

public class CollectionMappings : Profile
{
    public CollectionMappings()
    {
        CreateMap<CreateCollectionCommand, Collection>().ReverseMap();

        CreateMap<Collection, GetCollectionByIdQueryResponse>()
            .ForCtorParam(
                nameof(GetCollectionByIdQueryResponse.CollectibleIds),
                opt => opt.MapFrom(src => src.Collectibles.Select(c => c.Id))
            );

        CreateMap<UpdateCollectionCommand, Collection>().ReverseMap();

        CreateMap<Collection, GetCollectionsQueryResponse>()
            .ForCtorParam(
                nameof(GetCollectionByIdQueryResponse.CollectibleIds),
                opt => opt.MapFrom(src => src.Collectibles.Select(c => c.Id))
            );
    }
}
