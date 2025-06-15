namespace CollectR.Application.Features.Tags.Commands.UpdateTag;

internal sealed record UpdateTagCommandResponse(string Name, string Hex, Guid CollectionId);
