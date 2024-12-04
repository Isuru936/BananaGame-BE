using AutoMapper;
using BananaGame.Application.Abstractions.Messaging;
using BananaGame.Application.Abstractions.Persistence;
using BananaGame.Application.Features.GameSession.Query;
using BananaGame.Application.Features.GameSession.Response;
using BananaGame.Application.Shared;
using Microsoft.EntityFrameworkCore;
using Entity = BananaGame.Domain.Entities;

namespace BananaGame.Application.Features.GameSession.Handlers
{
    public class GetAllGameSessionsQueryHandler : IQueryHandler<GetAllGamesSessionsQuery, Result<IReadOnlyCollection<GameSessionResponse>>>
    {
        private readonly IGenericRepository<Entity.GameSession> _gameSessionRepository;
        private readonly IMapper _mapper;

        public GetAllGameSessionsQueryHandler(IMapper mapper, IGenericRepository<Entity.GameSession> gameSessionRepository)
        {
            _mapper = mapper;
            _gameSessionRepository = gameSessionRepository;
        }

        public async Task<Result<IReadOnlyCollection<GameSessionResponse>>> Handle(GetAllGamesSessionsQuery query, CancellationToken cancellationToken)
        {
            var gameSessions = await _gameSessionRepository.GetAll(q => q.Include(p => p.Player));

            var response = _mapper.Map<IReadOnlyCollection<GameSessionResponse>>(gameSessions);

            return Result.Success(_mapper.Map<IReadOnlyCollection<GameSessionResponse>>(gameSessions));
        }
    }
}
