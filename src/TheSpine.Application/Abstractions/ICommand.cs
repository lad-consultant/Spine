using MediatR;

namespace TheSpine.Application.Abstractions
{
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}
