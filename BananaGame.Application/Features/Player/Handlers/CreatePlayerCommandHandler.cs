using AutoMapper;
using BananaGame.Application.Abstractions.Messaging;
using BananaGame.Application.Abstractions.Persistence;
using BananaGame.Application.Features.Player.Commands;
using BananaGame.Application.Shared;

namespace BananaGame.Application.Features.Player.Handlers
{
    public class CreatePlayerCommandHandler : ICommandHandler<CreatePlayerCommand>
    {
        private readonly IGenericRepository<Domain.Entities.Player> _playerRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreatePlayerCommandHandler(IGenericRepository<Domain.Entities.Player> playerRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _playerRepository = playerRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<Result> Handle(CreatePlayerCommand command, CancellationToken cancellationToken)
        {
            Domain.Entities.Player player = Domain.Entities.Player.CreatePlayer(command.UserName, command.Email);

            var savedPlayer = await _playerRepository.AddAsync(player);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success("Player created successfully.");
        }
    }
}
