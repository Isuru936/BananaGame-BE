using BananaGame.Application.Shared;
using MediatR;

namespace BananaGame.Application.Abstractions.Messaging
{
    public interface ICommand : IRequest<Result> { }

    public interface ICommand<TResponse> : IRequest<Result<TResponse>> { }
}
