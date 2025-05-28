using MediatR;
using Microsoft.AspNetCore.Http;

namespace CollectR.Application.Features.Collectibles.Commands.UpdateCollectible;

public sealed record UpdateCollectibleCommand(IEnumerable<string>? ExistingImages, IFormFileCollection? NewImages, int Id) : IRequest<UpdateCollectibleCommandResponse>; // finish up all commands/queries and responses aswell
