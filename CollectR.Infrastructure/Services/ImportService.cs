using System.Text;
using System.Text.Json;
using System.Xml.Serialization;
using ClosedXML.Excel;
using CollectR.Application.Contracts.Models;
using CollectR.Application.Contracts.Persistence;
using CollectR.Application.Contracts.Services;
using CollectR.Domain;
using CollectR.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace CollectR.Infrastructure.Services;

public sealed class ImportService(IApplicationDbContext context) : IImportService
{
    public async Task<bool> ImportFromExcel(byte[] content, CancellationToken cancellationToken)
    {
        try
        {
            using var workbook = new XLWorkbook(new MemoryStream(content));

            var worksheet = workbook.Worksheets.FirstOrDefault();

            if (worksheet is null)
            {
                return false;
            }

            var collectionDto = WorkWithCollection.ParseWorksheet(worksheet);

            await ImportCollection(collectionDto, cancellationToken);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> ImportFromJson(byte[] content, CancellationToken cancellationToken)
    {
        try
        {
            var collectionDto = JsonSerializer.Deserialize<CollectionDto>(
                Encoding.UTF8.GetString(content),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            if (collectionDto is null)
            {
                return false;
            }

            await ImportCollection(collectionDto, cancellationToken);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> ImportFromXml(byte[] content, CancellationToken cancellationToken)
    {
        try
        {
            var serializer = new XmlSerializer(typeof(CollectionDto));

            using var ms = new MemoryStream(content);

            var collectionDto = (CollectionDto)serializer.Deserialize(ms);

            if (collectionDto is null)
            {
                return false;
            }

            await ImportCollection(collectionDto, cancellationToken);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private async Task ImportCollection(
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
                Attributes = new Attributes { Metadata = collectibleDto.Metadata },
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

        await context.SaveChangesAsync(cancellationToken);
    }
}
