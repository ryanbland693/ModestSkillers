using AutoMapper;
using DAL.ClanMember;
using RankChecker.BusinessLogic;
using WebAPI.DTO;

namespace WebAPI.Core
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ClanMemberRankCheck, ClanMemberRankCheckDTO>()
                .ForMember(
                    dest => dest.CurrentRank,
                    opt => opt.MapFrom(m => m.CurrentRank.ToString()))
                .ForMember(
                    dest => dest.ExpectedRank,
                    opt => opt.MapFrom(m => m.ExpectedRank.ToString()))
                .ForMember(
                    dest => dest.ClanXp,
                    opt => opt.MapFrom(m => m.ClanXp.ToString("n0")));
        }
    }
}
