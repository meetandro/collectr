using CollectR.Api.Infrastructure;
using CollectR.Application.Features.Categories.Commands.CreateCategory;
using CollectR.Application.Features.Categories.Commands.DeleteCategory;
using CollectR.Application.Features.Categories.Commands.UpdateCategory;
using CollectR.Application.Features.Categories.Queries.GetCategories;
using CollectR.Application.Features.Categories.Queries.GetCategoryById;
using MediatR;

namespace CollectR.Api.Endpoints;

public static class CategoryEndpoints
{
    public static void MapCategoryEndpoints(this IEndpointRouteBuilder app)
    {
        var root = app.MapGroup("categories");

        root.MapGet("", GetCategories);

        root.MapGet("{id}", GetCategoryById);

        root.MapPost("", CreateCategory);

        root.MapPut("{id}", UpdateCategory);

        root.MapDelete("{id}", DeleteCategory);
    }

    private static async Task<IResult> GetCategories(IMediator mediator)
    {
        var result = await mediator.Send(new GetCategoriesQuery());
        return ApiResult.FromResult(result);
    }

    private static async Task<IResult> GetCategoryById(Guid id, IMediator mediator)
    {
        var result = await mediator.Send(new GetCategoryByIdQuery(id));
        return ApiResult.FromResult(result);
    }

    private static async Task<IResult> CreateCategory(
        CreateCategoryCommand command,
        IMediator mediator
    )
    {
        var result = await mediator.Send(command);
        return ApiResult.FromResult(result);
    }

    private static async Task<IResult> UpdateCategory(
        UpdateCategoryCommand command,
        IMediator mediator
    )
    {
        var result = await mediator.Send(command);
        return ApiResult.FromResult(result);
    }

    private static async Task<IResult> DeleteCategory(Guid id, IMediator mediator)
    {
        var result = await mediator.Send(new DeleteCategoryCommand(id));
        return ApiResult.FromResult(result);
    }
}
