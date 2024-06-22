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
            if (member.Xp < 50_000_000)
            {
                return Rank.Recruit;
            }
            if (member.Xp < 100_000_000)
            {
                return Rank.Corporal; 
            }
            if (member.Xp < 200_000_000)
            {
                return Rank.Sergeant;
            }
            if (member.Xp < 300_000_000)
            {
                return Rank.Lieutenant;
            }
            if (member.Xp < 400_000_000)
            {
                return Rank.Captain;
            }
            if (member.Xp < 500_000_000)
            {
                return Rank.General;
            }
            return CheckAdminRank(member);
        }

        private Rank CheckAdminRank(ClanMember member) 
        {
            if (DateTime.UtcNow.AddYears(-2) > member.JoinDate)
            {
                return member.Xp > 500_000_000 ? Rank.Admin : Rank.General;
            }
            else if (DateTime.UtcNow.AddYears(-1) > member.JoinDate)
            {
                return member.Xp > 1_000_000_000 ? Rank.Admin : Rank.General;
            }
            return Rank.General;
        }

        public bool IsCorrectRank(ClanMember member) 
        {
            if ((int)member.Rank >= (int)Rank.Organiser) // organiser+
                return true;
            return GetXpRank(member) == member.Rank;
        }

        public async Task<List<ClanMemberRankCheck>> CheckRanksAsync(bool checkTooHigh, bool includeUnknownJoinDate)
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
                        ClanXp = member.Xp,
                        CurrentRank = member.Rank,
                        ExpectedRank = GetXpRank(member)
                    };
                    if (check.CurrentRank == Rank.General && member.JoinDate == DateTime.MinValue)
                    {
                        check.Message = "Check time in clan";
                    }
                    if (check.CurrentRank < check.ExpectedRank || checkTooHigh)
                    {
                        if (includeUnknownJoinDate) 
                        {
                            output.Add(check);
                        }
                        else if (member.JoinDate != DateTime.MinValue || check.ExpectedRank != Rank.Admin ) 
                        {
                            output.Add(check);
                        }
                    }
                }
            }
            return output.OrderBy(m => m.CurrentRank).ThenBy(m => m.Name).ToList();
        }
    }
}
