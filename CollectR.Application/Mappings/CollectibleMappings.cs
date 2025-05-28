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
            .ReverseMap();

        CreateMap<GetCollectibleByIdQueryResponse, Collectible>()
            .ReverseMap();

        CreateMap<UpdateCollectibleCommand, Collectible>()
            .ReverseMap();

        CreateMap<UpdateCollectibleCommandResponse, Collectible>()
            .ReverseMap();

        CreateMap<GetAllCollectiblesQueryResponse, Collectible>()
            .ReverseMap();
    }
}
