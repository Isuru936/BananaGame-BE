using AutoMapper;
using BananaGame.Application.Features.Player.Response;
using Entity = BananaGame.Domain.Entities;

namespace BananaGame.Application.Profiles
{
    public class PlayerProfile : Profile
    {
        public PlayerProfile()
        {
            CreateMap<Entity.Player, PlayerResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.FarthestLevel, opt => opt.MapFrom(src => src.FarthestLevel))
                .ForMember(dest => dest.LevelsPlayed, opt => opt.MapFrom(src => src.LevelsPlayed))
                .ForMember(dest => dest.TotalTimePlayed, opt => opt.MapFrom(src => src.TotalTimePlayed))
                .ForMember(dest => dest.HighestScore, opt => opt.MapFrom(src => src.HighestScore));
        }
    }
}
