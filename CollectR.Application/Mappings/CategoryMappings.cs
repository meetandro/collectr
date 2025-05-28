using AutoMapper;
using CollectR.Application.Features.Categories.Commands.CreateCategory;
using CollectR.Application.Features.Categories.Commands.UpdateCategory;
using CollectR.Application.Features.Categories.Queries.GetAllCategories;
using CollectR.Application.Features.Categories.Queries.GetCategoryById;
using CollectR.Domain;

namespace CollectR.Application.Mappings;

public class CategoryMappings : Profile // figure out correct mapping, p.s. figure logging
{
    public CategoryMappings()
    {
        CreateMap<CreateCategoryCommand, Category>()
            .ReverseMap();

        CreateMap<GetCategoryByIdQueryResponse, Category>()
            .ReverseMap();

        CreateMap<UpdateCategoryCommand, Category>()
            .ReverseMap();

        CreateMap<UpdateCategoryCommandResponse, Category>()
            .ReverseMap();

        CreateMap<GetAllCategoriesQueryResponse, Category>()
            .ReverseMap();
    }
}
