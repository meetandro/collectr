using MediatR;

namespace CollectR.Application.Abstractions.Messaging;

public interface IQuery<out TResponse> : IRequest<TResponse>;
