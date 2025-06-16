using MediatR;

namespace CollectR.Application.Abstractions;

public interface IQuery<out TResponse> : IRequest<TResponse>;
