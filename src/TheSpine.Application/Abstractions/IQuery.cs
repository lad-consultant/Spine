using MediatR;

namespace TheSpine.Application.Abstractions
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}
