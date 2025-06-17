using CollectR.Api.Infrastructure;
using CollectR.Application.Features.Categories.Commands.CreateCategory;
using CollectR.Application.Features.Categories.Commands.DeleteCategory;
using CollectR.Application.Features.Categories.Commands.UpdateCategory;
using CollectR.Application.Features.Categories.Queries.GetCategories;
using CollectR.Application.Features.Categories.Queries.GetCategoryById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CollectR.Api.Endpoints;

public static class CategoryEndpoints
{
    public static void MapCategoryEndpoints(this IEndpointRouteBuilder app)
    {
        var root = app.MapGroup("categories");

        root.MapGet("", GetAllCategories);

        root.MapGet("/{id}", GetCategoryById);

        root.MapPost("", CreateCategory);

        root.MapPut("/{id}", UpdateCategory);

        root.MapDelete("/{id}", DeleteCategory);
    }

    public static async Task<IResult> GetAllCategories(IMediator mediator)
    {
        var result = await mediator.Send(new GetCategoriesQuery());
        return Results.Ok(result);
    }

    public static async Task<IResult> GetCategoryById(Guid id, IMediator mediator)
    {
        var result = await mediator.Send(new GetCategoryByIdQuery(id));
        return ApiResult.FromResult(result);
    }

    public static async Task<IResult> CreateCategory(
        [FromBody] CreateCategoryCommand command,
        IMediator mediator
    )
    {
        var result = await mediator.Send(command);
        return ApiResult.FromResult(result);
    }

    public static async Task<IResult> UpdateCategory(
        [FromBody] UpdateCategoryCommand command,
        IMediator mediator
    )
    {
        var result = await mediator.Send(command);
        return ApiResult.FromResult(result);
    }

    public static async Task<IResult> DeleteCategory(Guid id, IMediator mediator)
    {
        var result = await mediator.Send(new DeleteCategoryCommand(id));
        return ApiResult.FromResult(result);
    }
}
