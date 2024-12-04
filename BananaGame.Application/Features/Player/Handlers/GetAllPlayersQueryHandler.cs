using AutoMapper;
using BananaGame.Application.Abstractions.Messaging;
using BananaGame.Application.Abstractions.Persistence;
using BananaGame.Application.Features.Player.Queries;
using BananaGame.Application.Features.Player.Response;
using BananaGame.Application.Shared;
using Microsoft.EntityFrameworkCore;
using Entity = BananaGame.Domain.Entities;

namespace BananaGame.Application.Features.Player.Handlers
{
    public class GetAllPlayersQueryHandler : IQueryHandler<GetAllPlayersQuery, Result<IReadOnlyCollection<PlayerResponse>>>
    {
        private readonly IGenericRepository<Entity.Player> _playerRepository;
        private readonly IMapper _mapper;

        public GetAllPlayersQueryHandler(IMapper mapper, IGenericRepository<Entity.Player> playerRepository)
        {
            _mapper = mapper;
            _playerRepository = playerRepository;
        }

        public async Task<Result<IReadOnlyCollection<PlayerResponse>>> Handle(GetAllPlayersQuery query, CancellationToken cancellationToken)
        {
            var players = await _playerRepository.GetAll(q => q.Include(p => p.GameSession));

            return Result.Success(_mapper.Map<IReadOnlyCollection<PlayerResponse>>(players));
        }
    }
}
