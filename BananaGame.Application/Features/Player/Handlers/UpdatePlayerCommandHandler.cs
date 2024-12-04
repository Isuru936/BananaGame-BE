using AutoMapper;
using BananaGame.Application.Abstractions.Messaging;
using BananaGame.Application.Abstractions.Persistence;
using BananaGame.Application.Features.Player.Commands;
using BananaGame.Application.Shared;
using Entity = BananaGame.Domain.Entities;

namespace BananaGame.Application.Features.Player.Handlers
{
    public class UpdatePlayerCommandHandler : ICommandHandler<UpdatePlayerCommand>
    {

        private readonly IGenericRepository<Entity.Player> _playerRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePlayerCommandHandler(IGenericRepository<Entity.Player> playerRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _playerRepository = playerRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result> Handle(UpdatePlayerCommand command, CancellationToken cancellationToken)
        {
            var player = await _playerRepository.GetByIdAsync(command.Id);

            if (player == null)
            {
                return Result.Failure(new Error("404", "Player Id Not Found"));
            }

            player.UserName = command.UserName;

            await _playerRepository.UpdateAsync(player);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success("Player Updated SuccessFully");

        }
    }
}
