using AutoMapper;
using BananaGame.Application.Abstractions.Messaging;
using BananaGame.Application.Abstractions.Persistence;
using BananaGame.Application.Features.Player.Queries;
using BananaGame.Application.Features.Player.Response;
using BananaGame.Application.Shared;
using Entity = BananaGame.Domain.Entities;

namespace BananaGame.Application.Features.Player.Handlers
{
    public class GetPlayerByIdQueryHandler : IQueryHandler<GetPlayerByIdQuery, Result<PlayerResponse>>
    {
        private readonly IGenericRepository<Entity.Player> _playerRepository;
        private readonly IMapper _mapper;

        public GetPlayerByIdQueryHandler(IGenericRepository<Entity.Player> playerRepository, IMapper mapper)
        {
            _playerRepository = playerRepository;
            _mapper = mapper;
        }

        public async Task<Result<PlayerResponse>> Handle(GetPlayerByIdQuery query, CancellationToken cancellationToken)
        {
            IReadOnlyCollection<Entity.Player> players = await _playerRepository.GetAllAsync();

            var player = players.FirstOrDefault(player => player.Id == query.Id);

            if (player == null)
            {
                return Result.Failure<PlayerResponse>(null, new Error("404", "Player Id Not Found"));
            }

            return Result.Success(_mapper.Map<PlayerResponse>(player));
        }
    }
}
