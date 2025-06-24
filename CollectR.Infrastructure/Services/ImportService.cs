using System.Text;
using System.Text.Json;
using System.Xml.Serialization;
using ClosedXML.Excel;
using CollectR.Application.Contracts.Models;
using CollectR.Application.Contracts.Persistence;
using CollectR.Application.Contracts.Services;
using CollectR.Domain;
using CollectR.Domain.Enums;
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

            var collectionDto = ParseWorksheet(worksheet);

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

    private static CollectionDto ParseWorksheet(IXLWorksheet worksheet)
    {
        var collection = new CollectionDto
        {
            Name = worksheet.Name,
            Description = worksheet.Cell(1, 1).GetString(),
            Collectibles = [],
        };

        int row = 3;

        while (!worksheet.Row(row).IsEmpty())
        {
            var title = worksheet.Cell(row, 1).GetString();

            if (string.IsNullOrWhiteSpace(title))
            {
                break;
            }

            var description = worksheet.Cell(row, 2).GetString();

            var currency = worksheet.Cell(row, 3).GetString();

            decimal? value = null;
            if (decimal.TryParse(worksheet.Cell(row, 4).GetString(), out var parsedValue))
            {
                value = parsedValue;
            }

            DateTime? acquiredDate = null;
            if (DateTime.TryParse(worksheet.Cell(row, 5).GetString(), out var parsedDate))
            {
                acquiredDate = parsedDate;
            }

            bool.TryParse(worksheet.Cell(row, 6).GetString(), out bool isCollected);

            int.TryParse(worksheet.Cell(row, 7).GetString(), out int sortIndex);

            Color? color = null;
            var colorString = worksheet.Cell(row, 8).GetString();
            if (Enum.TryParse<Color>(colorString, out var parsedColor))
            {
                color = parsedColor;
            }

            Condition? condition = null;
            var conditionString = worksheet.Cell(row, 9).GetString();
            if (Enum.TryParse<Condition>(conditionString, out var parsedCondition))
            {
                condition = parsedCondition;
            }

            var metadata = worksheet.Cell(row, 10).GetString();

            var category = worksheet.Cell(row, 11).GetString();

            var tagsString = worksheet.Cell(row, 12).GetString();

            var tags = new List<TagDto>();

            if (!string.IsNullOrWhiteSpace(tagsString))
            {
                var tagParts = tagsString.Split(',', StringSplitOptions.RemoveEmptyEntries);

                foreach (var part in tagParts)
                {
                    var trimmed = part.Trim();

                    int openParenthesesIndex = trimmed.LastIndexOf('(');
                    int closeParenthesesIndex = trimmed.LastIndexOf(')');

                    if (openParenthesesIndex > 0 && closeParenthesesIndex > openParenthesesIndex)
                    {
                        var name = trimmed[..openParenthesesIndex].Trim();

                        var hex = trimmed
                            .Substring(openParenthesesIndex + 1, closeParenthesesIndex - openParenthesesIndex - 1)
                            .Trim();

                        tags.Add(new TagDto { Name = name, Hex = hex });
                    }
                }
            }

            var collectible = new CollectibleDto
            {
                Title = title,
                Description = description,
                Currency = currency,
                Value = value,
                AcquiredDate = acquiredDate,
                IsCollected = isCollected,
                SortIndex = sortIndex,
                Color = color,
                Condition = condition,
                Metadata = metadata,
                Category = category,
                Tags = tags,
            };

            collection.Collectibles.Add(collectible);

            row++;
        }

        return collection;
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
