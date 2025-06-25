using CollectR.Application.Common.Format;
using CollectR.Application.Contracts.Models;
using CollectR.Application.Contracts.Persistence;
using CollectR.Application.Contracts.Services;
using CollectR.Domain;
using CollectR.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace CollectR.Infrastructure.Services;

public sealed class ImportService(IApplicationDbContext context) : IImportService
{
    public async Task<bool> ImportAsync(
        Format format,
        byte[] content,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var collectionDto = format switch
            {
                Format.Excel => CollectionParser.ParseExcel(content),
                Format.Json => CollectionParser.ParseJson(content),
                Format.Xml => CollectionParser.ParseXml(content),
                _ => null,
            };

            if (collectionDto is null)
            {
                return false;
            }

            return await ImportCollection(collectionDto, cancellationToken);
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> MergeAsync(
        Format format,
        byte[] content,
        Guid collectionId,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var collectionDto = format switch
            {
                Format.Excel => CollectionParser.ParseExcel(content),
                Format.Json => CollectionParser.ParseJson(content),
                Format.Xml => CollectionParser.ParseXml(content),
                _ => null,
            };

            if (collectionDto is null)
            {
                return false;
            }

            return await MergeCollection(collectionId, collectionDto, cancellationToken);
        }
        catch (Exception)
        {
            return false;
        }
    }

    private async Task<bool> ImportCollection(
        CollectionDto collectionDto,
        CancellationToken cancellationToken
    )
    {
        var collection = new Collection
        {
            Name = collectionDto.Name,
            Description = collectionDto.Description,
        };

        var categories = await context.Categories.ToListAsync(cancellationToken);

        var tags = new List<Tag>();

        foreach (var collectibleDto in collectionDto.Collectibles)
        {
            var category = categories.FirstOrDefault(c => c.Name == collectibleDto.Category);

            if (category is null)
            {
                category = new Category { Name = collectibleDto.Category };

                categories.Add(category);

                context.Categories.Add(category);
            }

            var collectible = new Collectible
            {
                Title = collectibleDto.Title,
                Description = collectibleDto.Description,
                Currency = collectibleDto.Currency,
                Value = collectibleDto.Value,
                AcquiredDate = collectibleDto.AcquiredDate,
                IsCollected = collectibleDto.IsCollected,
                SortIndex = collectibleDto.SortIndex,
                Color = collectibleDto.Color,
                Condition = collectibleDto.Condition,
                Category = category,
                Attributes = new Attributes { Metadata = collectibleDto.Metadata ?? "{}" },
                CategoryId = category.Id,
                CollectionId = collection.Id,
            };

            foreach (var tagDto in collectibleDto.Tags)
            {
                var tag = tags.FirstOrDefault(t => t.Name == tagDto.Name && t.Hex == tagDto.Hex);

                if (tag is null)
                {
                    tag = new Tag
                    {
                        Name = tagDto.Name,
                        Hex = tagDto.Hex,
                        CollectionId = collection.Id,
                    };

                    tags.Add(tag);

                    context.Tags.Add(tag);
                }

                collectible.CollectibleTags.Add(new CollectibleTag { Tag = tag });
            }

            collection.Collectibles.Add(collectible);
        }

        context.Collections.Add(collection);

        return true;
    }

    private async Task<bool> MergeCollection(
        Guid collectionId,
        CollectionDto collectionDto,
        CancellationToken cancellationToken
    )
    {
        var existingCollection = await context.Collections
            .Include(c => c.Collectibles)
            .ThenInclude(c => c.CollectibleTags)
            .Include(c => c.Collectibles)
            .ThenInclude(c => c.Category)
            .FirstOrDefaultAsync(c => c.Id == collectionId, cancellationToken);

        if (existingCollection is null)
        {
            return false;
        }

        var categories = await context.Categories.ToListAsync(cancellationToken);

        var tags = await context
            .Tags.Where(t => t.CollectionId == collectionId)
            .ToListAsync(cancellationToken);

        foreach (var collectibleDto in collectionDto.Collectibles)
        {
            var category = categories.FirstOrDefault(c => c.Name == collectibleDto.Category);

            if (category is null)
            {
                category = new Category { Name = collectibleDto.Category };

                categories.Add(category);

                context.Categories.Add(category);
            }

            var collectible = new Collectible
            {
                Title = collectibleDto.Title,
                Description = collectibleDto.Description,
                Currency = collectibleDto.Currency,
                Value = collectibleDto.Value,
                AcquiredDate = collectibleDto.AcquiredDate,
                IsCollected = collectibleDto.IsCollected,
                SortIndex = collectibleDto.SortIndex,
                Color = collectibleDto.Color,
                Condition = collectibleDto.Condition,
                Category = category,
                CategoryId = category.Id,
                CollectionId = existingCollection.Id,
                Attributes = new Attributes { Metadata = collectibleDto.Metadata ?? "{}" },
            };

            foreach (var tagDto in collectibleDto.Tags)
            {
                var tag = tags.FirstOrDefault(t => t.Name == tagDto.Name && t.Hex == tagDto.Hex);

                if (tag is null)
                {
                    tag = new Tag
                    {
                        Name = tagDto.Name,
                        Hex = tagDto.Hex,
                        CollectionId = existingCollection.Id,
                    };

                    tags.Add(tag);

                    context.Tags.Add(tag);
                }

                collectible.CollectibleTags.Add(new CollectibleTag { Tag = tag });
            }

            context.Collectibles.Add(collectible);
        }

        return true;
    }
}
