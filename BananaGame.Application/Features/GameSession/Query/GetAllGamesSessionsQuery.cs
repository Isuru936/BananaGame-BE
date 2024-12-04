using BananaGame.Application.Abstractions.Messaging;
using BananaGame.Application.Features.GameSession.Response;
using BananaGame.Application.Shared;

namespace BananaGame.Application.Features.GameSession.Query
{
    public class GetAllGamesSessionsQuery : IQuery<Result<IReadOnlyCollection<GameSessionResponse>>> { }
}
