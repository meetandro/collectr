using AutoMapper;
using CollectR.Application.Contracts.Models;
using CollectR.Application.Features.Collections.Commands.CreateCollection;
using CollectR.Application.Features.Collections.Commands.UpdateCollection;
using CollectR.Application.Features.Collections.Queries.GetCollectionById;
using CollectR.Application.Features.Collections.Queries.GetCollections;
using CollectR.Domain;

namespace CollectR.Application.Mappings;

internal sealed class CollectionMappings : Profile
{
    public CollectionMappings()
    {
        CreateMap<Collection, CollectionDto>();

        CreateMap<CreateCollectionCommand, Collection>();

        CreateMap<UpdateCollectionCommand, Collection>();

        CreateMap<Collection, GetCollectionsQueryResponse>()
            .ForCtorParam(
                nameof(GetCollectionsQueryResponse.CollectibleIds),
                opt => opt.MapFrom(src => src.Collectibles.Select(c => c.Id))
            );

        CreateMap<Collection, GetCollectionByIdQueryResponse>()
            .ForCtorParam(
                nameof(GetCollectionByIdQueryResponse.CollectibleIds),
                opt => opt.MapFrom(src => src.Collectibles.Select(c => c.Id))
            );
    }
}
