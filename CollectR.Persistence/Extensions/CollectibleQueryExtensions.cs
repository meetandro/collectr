using System.Linq.Expressions;
using CollectR.Domain;
using CollectR.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace CollectR.Persistence.Extensions;

internal static class CollectibleQueryExtensions
{
    public static IQueryable<Collectible> WhereSearchQuery(
        this IQueryable<Collectible> query,
        string? searchQuery
    )
    {
        if (string.IsNullOrWhiteSpace(searchQuery))
        {
            return query;
        }

        var like = $"%{searchQuery}%";

        return query.Where(c =>
            EF.Functions.Like(c.Title, like)
            || c.Description != null && EF.Functions.Like(c.Description, like)
        );
    }

    public static IQueryable<Collectible> WhereColors(
        this IQueryable<Collectible> query,
        string? colors
    )
    {
        if (string.IsNullOrWhiteSpace(colors))
        {
            return query;
        }

        var colorList = colors
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(c => c.Trim())
            .Select(c => Enum.TryParse<Color>(c, true, out var parsed) ? parsed : (Color?)null)
            .Where(c => c.HasValue)
            .Select(c => c.Value)
            .ToList();

        if (colorList.Count == 0)
        {
            return query;
        }

        return query.Where(c => c.Color != null && colorList.Contains(c.Color.Value));
    }

    public static IQueryable<Collectible> WhereCurrency(
        this IQueryable<Collectible> query,
        string? currency
    )
    {
        if (string.IsNullOrWhiteSpace(currency))
        {
            return query;
        }

        var like = $"%{currency}%";

        return query.Where(c => EF.Functions.Like(c.Currency, like));
    }

    public static IQueryable<Collectible> WhereMinValue(
        this IQueryable<Collectible> query,
        decimal? minValue
    )
    {
        return minValue is null ? query : query.Where(c => c.Value != null && c.Value >= minValue);
    }

    public static IQueryable<Collectible> WhereMaxValue(
        this IQueryable<Collectible> query,
        decimal? maxValue
    )
    {
        return maxValue is null ? query : query.Where(c => c.Value != null && c.Value <= maxValue);
    }

    public static IQueryable<Collectible> WhereConditions(
        this IQueryable<Collectible> query,
        string? conditions
    )
    {
        if (string.IsNullOrWhiteSpace(conditions))
        {
            return query;
        }

        var conditionList = conditions
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(c => c.Trim())
            .Select(c =>
                Enum.TryParse<Condition>(c, true, out var parsed) ? parsed : (Condition?)null
            )
            .Where(c => c.HasValue)
            .Select(c => c.Value)
            .ToList();

        if (conditionList.Count == 0)
        {
            return query;
        }

        return query.Where(c => c.Condition != null && conditionList.Contains(c.Condition.Value));
    }

    public static IQueryable<Collectible> WhereCategories(
        this IQueryable<Collectible> query,
        string? categoryIds
    )
    {
        if (string.IsNullOrWhiteSpace(categoryIds))
        {
            return query;
        }

        var categories = categoryIds
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(c => Guid.TryParse(c.Trim(), out var guid) ? guid : (Guid?)null)
            .Where(g => g.HasValue)
            .Select(g => g.Value)
            .ToList();

        if (categories.Count == 0)
        {
            return query;
        }

        return query.Where(c => categories.Contains(c.CategoryId));
    }

    public static IQueryable<Collectible> WhereTags(
        this IQueryable<Collectible> query,
        string? tagIds
    )
    {
        if (string.IsNullOrWhiteSpace(tagIds))
        {
            return query;
        }

        var tags = tagIds
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(t => Guid.TryParse(t.Trim(), out var guid) ? guid : (Guid?)null)
            .Where(g => g.HasValue)
            .Select(g => g.Value)
            .ToList();

        if (tags.Count == 0)
        {
            return query;
        }

        return query.Where(c => c.CollectibleTags.Any(ct => tags.Contains(ct.TagId)));
    }

    public static IQueryable<Collectible> WhereIsCollected(
        this IQueryable<Collectible> query,
        bool? isCollected
    )
    {
        return isCollected is null ? query : query.Where(c => c.IsCollected == isCollected);
    }

    public static IQueryable<Collectible> WhereAcquiredFrom(
        this IQueryable<Collectible> query,
        DateTime? acquiredFrom
    )
    {
        return acquiredFrom is null
            ? query
            : query.Where(c => c.AcquiredDate != null && c.AcquiredDate >= acquiredFrom);
    }

    public static IQueryable<Collectible> WhereAcquiredTo(
        this IQueryable<Collectible> query,
        DateTime? acquiredTo
    )
    {
        return acquiredTo is null
            ? query
            : query.Where(c => c.AcquiredDate != null && c.AcquiredDate <= acquiredTo);
    }

    public static IQueryable<Collectible> OrderBySort(
        this IQueryable<Collectible> query,
        string? sortBy,
        string? sortOrder
    )
    {
        Expression<Func<Collectible, object>>? keySelector = sortBy?.ToLower() switch
        {
            "title" => c => c.Title,
            "description" => c => c.Description,
            "color" => c => c.Color.Value,
            "currency" => c => c.Currency,
            "value" => c => (double)c.Value,
            "condition" => c => c.Condition.Value,
            "acquireddate" => c => c.AcquiredDate,
            _ => null,
        };

        if (keySelector is null)
        {
            return query.OrderBy(c => c.SortIndex);
        }

        return sortOrder?.ToLower() == "desc"
            ? query.OrderByDescending(keySelector).ThenBy(c => c.SortIndex)
            : query.OrderBy(keySelector).ThenBy(c => c.SortIndex);
    }
}
