using BananaGame.Application.Abstractions.Messaging;
using BananaGame.Application.Abstractions.Persistence;
using BananaGame.Application.Features.GameSession.Commands;
using BananaGame.Application.Shared;
using Entity = BananaGame.Domain.Entities;

namespace BananaGame.Application.Features.GameSession.Handlers
{
    // this is also not necessary, but lets have it for now
    public class UpdateGameSessionCommandHandler : ICommandHandler<UpdateGameSessionCommand>
    {
        private readonly IGenericRepository<Entity.GameSession> _gameSessionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateGameSessionCommandHandler(IUnitOfWork unitOfWork, IGenericRepository<Entity.GameSession> gameSessionRepository)
        {
            _unitOfWork = unitOfWork;
            _gameSessionRepository = gameSessionRepository;
        }

        public async Task<Result> Handle(UpdateGameSessionCommand command, CancellationToken cancellationToken)
        {
            Entity.GameSession? gameSession = await _gameSessionRepository.GetByIdAsync(command.PlayerId);

            if (gameSession == null)
            {
                return Result.Failure(new Error("404", $"GameSession Cannot be found with the ID {command.PlayerId}"));
            }

            gameSession.UpdateSession(command.SessionScore, command.EndDate);

            await _gameSessionRepository.UpdateAsync(gameSession);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success($"Session {gameSession.Id} is Updated");
        }
    }
}
