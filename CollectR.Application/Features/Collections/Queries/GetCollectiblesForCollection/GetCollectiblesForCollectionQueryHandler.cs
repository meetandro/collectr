using System.Linq.Expressions;
using AutoMapper;
using CollectR.Application.Abstractions;
using CollectR.Application.Common;
using CollectR.Application.Contracts.Persistence;
using CollectR.Domain;
using CollectR.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace CollectR.Application.Features.Collections.Queries.GetCollectiblesForCollection;

internal sealed class GetCollectiblesForCollectionQueryHandler(
    IApplicationDbContext context,
    IMapper mapper
)
    : IQueryHandler<
        GetCollectiblesForCollectionQuery,
        PaginatedList<GetCollectiblesForCollectionQueryResponse>
    >
{
    public async Task<PaginatedList<GetCollectiblesForCollectionQueryResponse>> Handle(
        GetCollectiblesForCollectionQuery request,
        CancellationToken cancellationToken
    )
    {
        IEnumerable<Collectible> collectibles = await context.Collectibles
            .Where(c => c.CollectionId == request.Id)
            .Include(c => c.Images)
            .Include(c => c.CollectibleTags)
            .AsSplitQuery() // check if this will mess legit everything up
            .AsNoTracking() // projectto mapper
            .ToListAsync(cancellationToken);

        if (!string.IsNullOrEmpty(request.Query))
        {
            collectibles = collectibles.Where(c =>
                c.Title.Contains(request.Query.ToLower(), StringComparison.CurrentCultureIgnoreCase)
                || (
                    c.Description != null
                    && c.Description.Contains(
                        request.Query.ToLower(),
                        StringComparison.CurrentCultureIgnoreCase
                    )
                )
            );
        }

        List<Color>? colorsList = null;
        if (!string.IsNullOrEmpty(request.Colors))
        {
            colorsList = request
                .Colors.Split(',')
                .Select(color =>
                    Enum.TryParse<Color>(color, true, out var parsedColor)
                        ? parsedColor
                        : (Color?)null
                )
                .Where(c => c.HasValue)
                .Select(c => c.Value)
                .ToList();
        }
        if (colorsList is not null && colorsList.Count != 0)
        {
            collectibles = collectibles.Where(c =>
                c.Color != null && colorsList.Contains(c.Color.Value)
            );
        }

        if (!string.IsNullOrEmpty(request.Currency))
        {
            collectibles = collectibles.Where(c =>
                c.Currency != null
                && c.Currency.Contains(
                    request.Currency.ToLower(),
                    StringComparison.CurrentCultureIgnoreCase
                )
            );
        }

        if (request.MinValue is not null)
        {
            collectibles = collectibles.Where(c => c.Value != null && c.Value >= request.MinValue);
        }

        if (request.MaxValue is not null)
        {
            collectibles = collectibles.Where(c => c.Value != null && c.Value <= request.MaxValue);
        }

        List<Condition>? conditionsList = null;
        if (!string.IsNullOrEmpty(request.Conditions))
        {
            conditionsList = request
                .Conditions.Split(',')
                .Select(cond =>
                    Enum.TryParse<Condition>(cond, true, out var parsedCondition)
                        ? parsedCondition
                        : (Condition?)null
                )
                .Where(c => c.HasValue)
                .Select(c => c.Value)
                .ToList();
        }
        if (conditionsList is not null && conditionsList.Count != 0)
        {
            collectibles = collectibles.Where(c =>
                c.Condition != null && conditionsList.Contains(c.Condition.Value)
            );
        }

        List<Guid>? categoryIds = null;
        if (!string.IsNullOrEmpty(request.Categories))
        {
            categoryIds = request.Categories.Split(',').Select(Guid.Parse).ToList();
        }
        if (categoryIds is not null && categoryIds.Count != 0)
        {
            collectibles = collectibles.Where(c => categoryIds.Contains(c.CategoryId));
        }

        List<Guid>? tagIds = null;
        if (!string.IsNullOrEmpty(request.Tags))
        {
            tagIds = request.Tags.Split(',').Select(Guid.Parse).ToList();
        }
        if (tagIds is not null && tagIds.Count != 0)
        {
            collectibles = collectibles.Where(c =>
                c.CollectibleTags.Any(ct => tagIds.Contains(ct.TagId))
            );
        }

        if (request.AcquiredFrom is not null)
        {
            collectibles = collectibles.Where(c =>
                c.AcquiredDate != null && c.AcquiredDate >= request.AcquiredFrom
            );
        }

        if (request.AcquiredTo is not null)
        {
            collectibles = collectibles.Where(c =>
                c.AcquiredDate != null && c.AcquiredDate <= request.AcquiredTo
            );
        }

        if (request.IsCollected is not null)
        {
            collectibles = collectibles.Where(c =>
                c.IsCollected == request.IsCollected
            );
        }

        Expression<Func<Collectible, object>> keySelector = request.SortBy?.ToLower() switch
        {
            "title" => collectible => collectible.Title,
            "description" => collectible => collectible.Description,
            "color" => collectible => collectible.Color.Value,
            "currency" => collectible => collectible.Currency,
            "value" => collectible => collectible.Value,
            "condition" => collectible => collectible.Condition.Value,
            "acquireddate" => collectible => collectible.AcquiredDate,
            _ => collectible => collectible.Id,
        };

        if (request.SortOrder == "desc")
        {
            collectibles = collectibles.AsQueryable().OrderByDescending(keySelector);
        }
        else if (request.SortOrder == "asc")
        {
            collectibles = collectibles.AsQueryable().OrderBy(keySelector);
        }

        var count = collectibles.Count();

        var pagedCollectibles = collectibles
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToList();

        var collectiblesResponse = pagedCollectibles.Select(
            mapper.Map<GetCollectiblesForCollectionQueryResponse>
        );

        return new PaginatedList<GetCollectiblesForCollectionQueryResponse>(
            collectiblesResponse,
            count,
            request.Page,
            request.PageSize
        );
    }
}
