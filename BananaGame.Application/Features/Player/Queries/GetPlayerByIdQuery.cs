using BananaGame.Application.Abstractions.Messaging;
using BananaGame.Application.Features.Player.Response;
using BananaGame.Application.Shared;

namespace BananaGame.Application.Features.Player.Queries
{
    public record GetPlayerByIdQuery(Guid Id) : IQuery<Result<PlayerResponse>> { }
}
