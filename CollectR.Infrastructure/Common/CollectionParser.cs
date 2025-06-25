using System.Text;
using System.Text.Json;
using System.Xml.Serialization;
using ClosedXML.Excel;
using CollectR.Application.Contracts.Models;

namespace CollectR.Infrastructure.Common;

internal static class CollectionParser
{
    public static CollectionDto? ParseJson(byte[] content)
    {
        var collectionDto = JsonSerializer.Deserialize<CollectionDto>(
            Encoding.UTF8.GetString(content),
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        );

        return collectionDto;
    }

    public static CollectionDto? ParseExcel(byte[] content)
    {
        using var workbook = new XLWorkbook(new MemoryStream(content));

        var worksheet = workbook.Worksheets.FirstOrDefault();

        if (worksheet is null)
        {
            return null;
        }

        var collectionDto = WorkWithCollection.ParseWorksheet(worksheet);

        return collectionDto;
    }

    public static CollectionDto? ParseXml(byte[] content)
    {
        var serializer = new XmlSerializer(typeof(CollectionDto));

        using var ms = new MemoryStream(content);

        var collectionDto = (CollectionDto)serializer.Deserialize(ms);

        return collectionDto;
    }
}
