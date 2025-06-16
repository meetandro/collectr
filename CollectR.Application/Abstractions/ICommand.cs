using MediatR;

namespace CollectR.Application.Abstractions;

public interface ICommand<out TResponse> : IRequest<TResponse>;
