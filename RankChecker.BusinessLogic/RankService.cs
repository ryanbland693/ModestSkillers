using DAL.ClanMember;

namespace RankChecker.BusinessLogic
{
    public class RankService
    {
        private readonly IClanMemberRepo repo;

        public RankService(IClanMemberRepo repo)
        {
            this.repo = repo;
        }

        public Rank GetXpRank(ClanMember member)
        {
            if (member.ClanXp < 50_000_000)
            {
                return Rank.Recruit;
            }
            if (member.ClanXp < 100_000_000)
            {
                return Rank.Corporal; 
            }
            if (member.ClanXp < 200_000_000)
            {
                return Rank.Sergeant;
            }
            if (member.ClanXp < 300_000_000)
            {
                return Rank.Lieutenant;
            }
            if (member.ClanXp < 400_000_000)
            {
                return Rank.Captain;
            }
            if (member.ClanXp < 500_000_000)
            {
                return Rank.General;
            }
            return Rank.Admin;
        }

        public bool IsCorrectRank(ClanMember member) 
        {
            if ((int)member.Rank >= (int)Rank.Organiser) // organiser+
                return true;
            return GetXpRank(member) == member.Rank;
        }

        public async Task<List<ClanMemberRankCheck>> CheckRanksAsync()
        {
            var output = new List<ClanMemberRankCheck>();
            var members = await repo.GetMembersAsync();
            foreach (var member in members)
            {
                if (!IsCorrectRank(member))
                {
                    var check = new ClanMemberRankCheck
                    {
                        Name = member.Name,
                        ClanXp = member.ClanXp,
                        CurrentRank = member.Rank,
                        ExpectedRank = GetXpRank(member)
                    };
                    if (check.CurrentRank == Rank.General && check.ExpectedRank == Rank.Admin)
                    {
                        check.Message = "Check time in clan";
                    } 
                    output.Add(check);
                }
            }
            return output;
        }
    }
}
