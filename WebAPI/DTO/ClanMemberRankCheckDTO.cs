using DAL.ClanMember;

namespace WebAPI.DTO
{
    public class ClanMemberRankCheckDTO
    {
        public string Name { get; set; } = string.Empty;
        public string ClanXp { get; set; } = string.Empty;
        public string CurrentRank { get; set; } = string.Empty;
        public string ExpectedRank { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
