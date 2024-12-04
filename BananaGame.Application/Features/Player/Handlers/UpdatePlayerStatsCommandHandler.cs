using BananaGame.Application.Abstractions.Messaging;
using BananaGame.Application.Abstractions.Persistence;
using BananaGame.Application.Features.Player.Commands;
using BananaGame.Application.Features.Player.Response;
using BananaGame.Application.Shared;
using Entity = BananaGame.Domain.Entities;

namespace BananaGame.Application.Features.Player.Handlers
{
    // I dont this is necessary, but lets have it for now
    public class UpdatePlayerStatsCommandHandler : ICommandHandler<UpdatePlayerStatsCommand>
    {
        private readonly IGenericRepository<Entity.Player> _playerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePlayerStatsCommandHandler(IUnitOfWork unitOfWork, IGenericRepository<Entity.Player> playerRepository)
        {
            _unitOfWork = unitOfWork;
            _playerRepository = playerRepository;
        }

        public async Task<Result> Handle(UpdatePlayerStatsCommand command, CancellationToken cancellationToken)
        {
            Entity.Player? player = await _playerRepository.GetByIdAsync(command.Id);
            if (player == null)
            {
                return Result.Failure<PlayerResponse>(null, new Error("404", "Player Id Not Found"));
            }

            player.UpdatePlayerStats
                (command.FarthestLevel, command.LevelsPlayed, command.TotalTimePlayed, command.HighestScore);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
