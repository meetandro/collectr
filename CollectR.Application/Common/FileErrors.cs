namespace CollectR.Application.Common;

public static class FileErrors
{
    public static Error UnsupportedFormat(string format) =>
        new("File.UnsupportedFormat", $"Format {format} is unsupported.");

    public static Error ImportingFailed() => new("File.ImportingFailed", "Failed importing file.");
}
