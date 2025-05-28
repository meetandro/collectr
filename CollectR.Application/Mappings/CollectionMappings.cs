using AutoMapper;
using CollectR.Application.Features.Collections.Commands.CreateCollection;
using CollectR.Application.Features.Collections.Commands.UpdateCollection;
using CollectR.Application.Features.Collections.Queries.GetAllCollections;
using CollectR.Application.Features.Collections.Queries.GetCollectionById;
using CollectR.Domain;

namespace CollectR.Application.Mappings;

public class CollectionMappings : Profile
{
    public CollectionMappings()
    {
        CreateMap<CreateCollectionCommand, Collection>()
            .ReverseMap();

        CreateMap<GetCollectionByIdQueryResponse, Collection>()
            .ReverseMap();

        CreateMap<UpdateCollectionCommand, Collection>()
            .ReverseMap();

        CreateMap<UpdateCollectionCommandResponse, Collection>()
            .ReverseMap();

        CreateMap<GetAllCollectionsQueryResponse, Collection>()
            .ReverseMap();
    }
}
