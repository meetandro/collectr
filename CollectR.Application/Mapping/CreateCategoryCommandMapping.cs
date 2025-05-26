using AutoMapper;
using CollectR.Application.Features.Categories.Commands.CreateCategory;
using CollectR.Application.Features.Categories.Queries.GetCategoryById;
using CollectR.Domain;

namespace CollectR.Application.Mapping;

public class MappingProfiles : Profile // figure out correct mapping, p.s. figure logging
{
    public MappingProfiles()
    {
        CreateMap<CreateCategoryCommand, Category>()
            .ReverseMap();

        CreateMap<GetCategoryByIdQueryResponse, Category>()
            .ReverseMap();
    }
}
