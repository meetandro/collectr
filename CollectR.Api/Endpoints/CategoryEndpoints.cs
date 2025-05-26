using CollectR.Application.Features.Categories.Commands.CreateCategory;
using CollectR.Application.Features.Categories.Queries.GetCategoryById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CollectR.Api.Endpoints;

public static class CategoryEndpoints
{
    public static void MapCategoryEndpoints(this WebApplication app)
    {
        var root = app.MapGroup("/api/categories");

        root.MapPost("", CreateCategory);
        root.MapGet("id", GetCategoryById);
    }

    public static async Task<IResult> CreateCategory(
        [FromBody] CreateCategoryCommand command,
        IMediator mediator
    )
    {
        var result = await mediator.Send(command);
        return Results.Ok(result);
    }

    public static async Task<IResult> GetCategoryById(int id, IMediator mediator)
    {
        var result = await mediator.Send(new GetCategoryByIdQuery(id));
        return Results.Ok(result);
    }
}
