using ClosedXML.Excel;
using CollectR.Application.Contracts.Models;
using CollectR.Domain.Enums;

namespace CollectR.Infrastructure.Common;

internal static class WorkWithCollection
{
    public static IXLWorksheet AddWorksheet(XLWorkbook workbook, CollectionDto collectionDto)
    {
        var worksheet = workbook.Worksheets
            .Add(collectionDto.Name)
            .SetTabColor(XLColor.CornflowerBlue);

        worksheet.Cell(1, 1).Value = collectionDto?.Description ?? "Collection";

        worksheet.Cell(2, 1).Value = "Title";
        worksheet.Cell(2, 2).Value = "Description";
        worksheet.Cell(2, 3).Value = "Currency";
        worksheet.Cell(2, 4).Value = "Value";
        worksheet.Cell(2, 5).Value = "Acquired Date";
        worksheet.Cell(2, 6).Value = "Is Collected";
        worksheet.Cell(2, 7).Value = "Sort Index";
        worksheet.Cell(2, 8).Value = "Color";
        worksheet.Cell(2, 9).Value = "Condition";
        worksheet.Cell(2, 10).Value = "Metadata";
        worksheet.Cell(2, 11).Value = "Category";
        worksheet.Cell(2, 12).Value = "Tags";

        worksheet.Row(2).Style.Font.Bold = true;

        int row = 3;

        foreach (var collectibleDto in collectionDto.Collectibles)
        {
            worksheet.Cell(row, 1).Value = collectibleDto.Title;
            worksheet.Cell(row, 2).Value = collectibleDto.Description;
            worksheet.Cell(row, 3).Value = collectibleDto.Currency;
            worksheet.Cell(row, 4).Value = collectibleDto.Value;
            worksheet.Cell(row, 5).Value = collectibleDto.AcquiredDate?.ToString("yyyy-MM-dd") ?? "";
            worksheet.Cell(row, 6).Value = collectibleDto.IsCollected;
            worksheet.Cell(row, 7).Value = collectibleDto.SortIndex;
            worksheet.Cell(row, 8).Value = collectibleDto.Color?.ToString() ?? "";
            worksheet.Cell(row, 9).Value = collectibleDto.Condition?.ToString() ?? "";
            worksheet.Cell(row, 10).Value = collectibleDto.Metadata;
            worksheet.Cell(row, 11).Value = collectibleDto.Category;

            var tags =
                collectibleDto.Tags.Count > 0
                    ? string.Join(", ", collectibleDto.Tags.Select(t => $"{t.Name} ({t.Hex})"))
                    : "";

            worksheet.Cell(row, 12).Value = tags;

            row++;
        }

        worksheet.Columns().AdjustToContents();

        return worksheet;
    }
    
    public static CollectionDto ParseWorksheet(IXLWorksheet worksheet)
    {
        var collectionDto = new CollectionDto
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

            var tagDtos = new List<TagDto>();

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

                        tagDtos.Add(new TagDto { Name = name, Hex = hex });
                    }
                }
            }

            var collectibleDto = new CollectibleDto
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
                Tags = tagDtos,
            };

            collectionDto.Collectibles.Add(collectibleDto);

            row++;
        }

        return collectionDto;
    }
}
