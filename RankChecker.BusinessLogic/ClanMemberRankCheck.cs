using DAL.ClanMember;

namespace RankChecker.BusinessLogic
{
    public class ClanMemberRankCheck
    {
        public string Name { get; set; } = string.Empty;
        public long ClanXp { get; set; }
        public Rank CurrentRank { get; set; }
        public Rank ExpectedRank { get; set; }
        public string Message { get; set; }
    }
}
