namespace CollectR.Application.Common.Format;

public static class FormatHelper
{
    public static Format GetFormatFromString(string? format) => format?.ToLowerInvariant() switch
    {
        "json" => Format.Json,
        ".json" => Format.Json,
        "xml" => Format.Xml,
        ".xml" => Format.Xml,
        "excel" => Format.Excel,
        ".xlsx" => Format.Excel,
        ".xls" => Format.Excel,
        _ => Format.Unknown
    };
}
