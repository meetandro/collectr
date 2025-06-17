using AutoMapper;
using CollectR.Application.Features.Categories.Commands.CreateCategory;
using CollectR.Application.Features.Categories.Commands.UpdateCategory;
using CollectR.Application.Features.Categories.Queries.GetCategories;
using CollectR.Application.Features.Categories.Queries.GetCategoryById;
using CollectR.Domain;

namespace CollectR.Application.Mappings;

internal sealed class CategoryMappings : Profile
{
    public CategoryMappings()
    {
        CreateMap<CreateCategoryCommand, Category>();

        CreateMap<UpdateCategoryCommand, Category>();

        CreateMap<Category, GetCategoriesQueryResponse>()
            .ForCtorParam(
                nameof(GetCategoriesQueryResponse.CollectibleIds),
                opt => opt.MapFrom(src => src.Collectibles.Select(c => c.Id))
            );

        CreateMap<Category, GetCategoryByIdQueryResponse>()
            .ForCtorParam(
                nameof(GetCategoryByIdQueryResponse.CollectibleIds),
                opt => opt.MapFrom(src => src.Collectibles.Select(c => c.Id))
            );
    }
}
