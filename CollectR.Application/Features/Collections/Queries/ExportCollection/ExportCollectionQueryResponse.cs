namespace CollectR.Application.Features.Collections.Queries.ExportCollection;

public sealed record ExportCollectionQueryResponse(byte[] FileContents, string ContentType, string FileName);
