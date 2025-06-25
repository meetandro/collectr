using System.Text;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;
using ClosedXML.Excel;
using CollectR.Application.Common.Format;
using CollectR.Application.Contracts.Models;
using CollectR.Application.Contracts.Services;
using CollectR.Infrastructure.Common;

namespace CollectR.Infrastructure.Services;

public sealed class ExportService : IExportService
{
    public byte[] Export(Format format, CollectionDto collection)
    {
        return format switch
        {
            Format.Excel => ExportAsExcel(collection),
            Format.Json => ExportAsJson(collection),
            Format.Xml => ExportAsXml(collection),
            _ => throw new NotSupportedException("Unsupported format."),
        };
    }

    private static byte[] ExportAsExcel(CollectionDto collectionDto)
    {
        using var workbook = new XLWorkbook();

        WorkWithCollection.AddWorksheet(workbook, collectionDto);

        using var stream = new MemoryStream();

        workbook.SaveAs(stream);

        return stream.ToArray();
    }

    private static byte[] ExportAsJson(CollectionDto collectionDto)
    {
        var json = JsonSerializer.Serialize(
            collectionDto,
            new JsonSerializerOptions { WriteIndented = true }
        );

        return Encoding.UTF8.GetBytes(json);
    }

    private static byte[] ExportAsXml(CollectionDto collectionDto)
    {
        var serializer = new XmlSerializer(typeof(CollectionDto));

        using var ms = new MemoryStream();

        using (
            var xmlWriter = XmlWriter.Create(
                ms,
                new XmlWriterSettings
                {
                    Encoding = Encoding.UTF8,
                    Indent = true,
                    OmitXmlDeclaration = false,
                }
            )
        )
        {
            serializer.Serialize(xmlWriter, collectionDto);
        }

        return ms.ToArray();
    }
}
