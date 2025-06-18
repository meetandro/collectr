using ClosedXML.Excel;
using CollectR.Application.Contracts.Models;

namespace CollectR.Infrastructure.Common;

internal static class WorkWithCollection
{
    public static IXLWorksheet AddWorksheet(XLWorkbook workbook, CollectionDto collection)
    {
        var worksheet = workbook
            .Worksheets.Add(collection.Name)
            .SetTabColor(XLColor.CornflowerBlue);

        worksheet.Cell(1, 1).Value = collection?.Description ?? "Collection";

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

        foreach (var collectible in collection.Collectibles)
        {
            worksheet.Cell(row, 1).Value = collectible.Title;
            worksheet.Cell(row, 2).Value = collectible.Description;
            worksheet.Cell(row, 3).Value = collectible.Currency;
            worksheet.Cell(row, 4).Value = collectible.Value;
            worksheet.Cell(row, 5).Value = collectible.AcquiredDate?.ToString("yyyy-MM-dd") ?? "";
            worksheet.Cell(row, 6).Value = collectible.IsCollected;
            worksheet.Cell(row, 7).Value = collectible.SortIndex;
            worksheet.Cell(row, 8).Value = collectible.Color?.ToString() ?? "";
            worksheet.Cell(row, 9).Value = collectible.Condition?.ToString() ?? "";
            worksheet.Cell(row, 10).Value = collectible.Metadata;
            worksheet.Cell(row, 11).Value = collectible.Category;

            var tags =
                collectible.Tags.Count > 0
                    ? string.Join(", ", collectible.Tags.Select(t => $"{t.Name} ({t.Hex})"))
                    : "";

            worksheet.Cell(row, 12).Value = tags;

            row++;
        }

        worksheet.Columns().AdjustToContents();

        return worksheet;
    }
}
