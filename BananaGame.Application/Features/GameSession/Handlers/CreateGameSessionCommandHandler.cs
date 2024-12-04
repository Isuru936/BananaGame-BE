using BananaGame.Application.Abstractions.Messaging;
using BananaGame.Application.Abstractions.Persistence;
using BananaGame.Application.Features.GameSession.Commands;
using BananaGame.Application.Shared;
using Microsoft.EntityFrameworkCore;
using Entity = BananaGame.Domain.Entities;

namespace BananaGame.Application.Features.GameSession.Handlers
{
    public class CreateGameSessionCommandHandler : ICommandHandler<StartGameSessionCommand>
    {
        private readonly IGenericRepository<Entity.GameSession> _gameSessionRepository;
        private readonly IGenericRepository<Entity.Player> _playerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateGameSessionCommandHandler(IUnitOfWork unitOfWork, IGenericRepository<Entity.GameSession> gameSessionRepository, IGenericRepository<Entity.Player> playerRepository)
        {
            _gameSessionRepository = gameSessionRepository;
            _playerRepository = playerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(StartGameSessionCommand command, CancellationToken cancellationToken)
        {
            Entity.Player? player = await _playerRepository.GetByIdAsync(command.PlayerId);

            var playerWithSession = await _playerRepository.Get(command.PlayerId, q => q.Include(p => p.GameSession));


            if (playerWithSession?.GameSession != null)
            {
                return Result.Failure(new Error("404", $"Player ID Already exists {command.PlayerId}"));
            }

            Entity.GameSession gameSession = Entity.GameSession.CreateSession(command.PlayerId, player!);

            await _gameSessionRepository.AddAsync(gameSession);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success($"Session {gameSession.Id} is Created");
        }
    }
}
