using System.Text;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;
using ClosedXML.Excel;
using CollectR.Application.Contracts.Models;
using CollectR.Application.Contracts.Services;
using CollectR.Infrastructure.Common;

namespace CollectR.Infrastructure.Services;

public sealed class ExportService : IExportService
{
    public Task<byte[]> ExportAsExcel(CollectionDto collection)
    {
        using var workbook = new XLWorkbook();

        WorkWithCollection.AddWorksheet(workbook, collection);

        using var stream = new MemoryStream();

        workbook.SaveAs(stream);

        return Task.FromResult(stream.ToArray());
    }

    public Task<byte[]> ExportAsJson(CollectionDto collection)
    {
        var json = JsonSerializer.Serialize(
            collection,
            new JsonSerializerOptions { WriteIndented = true }
        );

        return Task.FromResult(Encoding.UTF8.GetBytes(json));
    }

    public Task<byte[]> ExportAsXml(CollectionDto collection)
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
            serializer.Serialize(xmlWriter, collection);
        }

        return Task.FromResult(ms.ToArray());
    }
}
