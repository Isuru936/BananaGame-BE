using MediatR;

namespace BananaGame.Application.Abstractions.Messaging
{
    public interface IQuery<TResponse> : IRequest<TResponse> { }
}
