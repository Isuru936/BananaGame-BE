using AutoMapper;
using BananaGame.Application.Features.GameSession.Response;
using BananaGame.Domain.Entities;

namespace BananaGame.Application.Profiles
{
    public class GameSessionProfile : Profile
    {
        public GameSessionProfile()
        {
            CreateMap<GameSession, GameSessionResponse>()
                .ForMember(dest => dest.PlayerId, opt => opt.MapFrom(GameSession => GameSession.PlayerId))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(GameSession => GameSession.StartTime))
                .ForMember(dest => dest.SessionScore, opt => opt.MapFrom(GameSession => GameSession.SessionScore));
        }
    }
}
