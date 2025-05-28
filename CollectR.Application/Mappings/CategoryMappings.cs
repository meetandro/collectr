using AutoMapper;
using CollectR.Application.Features.Categories.Commands.CreateCategory;
using CollectR.Application.Features.Categories.Commands.UpdateCategory;
using CollectR.Application.Features.Categories.Queries.GetAllCategories;
using CollectR.Application.Features.Categories.Queries.GetCategoryById;
using CollectR.Domain;

namespace CollectR.Application.Mappings;

public class CategoryMappings : Profile
{
    public CategoryMappings()
    {
        CreateMap<CreateCategoryCommand, Category>()
            .ReverseMap();

        CreateMap<Category, GetCategoryByIdQueryResponse>()
            .ForCtorParam(nameof(GetCategoryByIdQueryResponse.CollectibleIds), opt => opt.MapFrom(src => src.Collectibles.Select(c => c.Id)));

        CreateMap<UpdateCategoryCommand, Category>()
    .ReverseMap();

CreateMap<UpdateCategoryCommandResponse, Category>()
    .ReverseMap();

    CreateMap<Category, GetAllCategoriesQueryResponse>()
        .ForCtorParam(nameof(GetCategoryByIdQueryResponse.CollectibleIds), opt => opt.MapFrom(src => src.Collectibles.Select(c => c.Id)))
        .ReverseMap(); // good but should you return this in the first place? IEnumerable<Response> or Response
}
}
