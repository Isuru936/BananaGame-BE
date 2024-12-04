using MediatR;

namespace BananaGame.Application.Abstractions.Messaging
{
    public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, TResponse> where TQuery : IQuery<TResponse>
    {
        new Task<TResponse> Handle(TQuery query, CancellationToken cancellationToken);
    }
}
