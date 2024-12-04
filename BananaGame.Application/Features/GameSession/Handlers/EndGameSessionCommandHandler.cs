using BananaGame.Application.Abstractions.Messaging;
using BananaGame.Application.Abstractions.Persistence;
using BananaGame.Application.Features.GameSession.Commands;
using BananaGame.Application.Shared;
using Microsoft.EntityFrameworkCore;
using Entity = BananaGame.Domain.Entities;

namespace BananaGame.Application.Features.GameSession.Handlers
{
    public class EndGameSessionCommandHandler : ICommandHandler<EndGameSessionCommand>
    {
        private readonly IGenericRepository<Entity.Player> _playerRepository;
        private readonly IGenericRepository<Entity.GameSession> _gameSessionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EndGameSessionCommandHandler(IGenericRepository<Entity.Player> playerSessionRepository, IUnitOfWork unitOfWork, IGenericRepository<Entity.GameSession> gameSessionRepository)
        {
            _playerRepository = playerSessionRepository;
            _unitOfWork = unitOfWork;
            _gameSessionRepository = gameSessionRepository;
        }

        public async Task<Result> Handle(EndGameSessionCommand command, CancellationToken cancellationToken)
        {
            Entity.Player? player = await _playerRepository.Get(command.PlayerId, q => q.Include(p => p.GameSession));

            if (player == null)
            {
                return Result.Failure(new Error("404", $"GameSession Cannot be found with the Player ID {command.PlayerId}"));
            }

            Entity.GameSession? currentSession = await _gameSessionRepository.Get(id: player.GameSession!.Id);

            // calculates the player stats before ending the session
            DateTime newTotalTimePlayed = player.TotalTimePlayed + (DateTime.UtcNow - player.GameSession!.StartTime);
            int newHihghestScore = player.HighestScore < command.SessionScore ? command.SessionScore : player.HighestScore;
            int farthestLevel = player.FarthestLevel < command.Level ? command.Level : player.FarthestLevel;
            int totalLevelsPlayed = player.LevelsPlayed + command.Level;

            player.UpdatePlayerStats(farthestLevel, totalLevelsPlayed, newTotalTimePlayed, newHihghestScore);

            await _playerRepository.UpdateAsync(player);
            await _gameSessionRepository.DeleteAsync(currentSession!);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success($"Session {player.Id} is Ended");
        }
    }
}
