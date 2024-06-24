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

        private bool IsYearsInClan(ClanMember member, int years)
        {
            return DateTime.UtcNow.AddYears(-1 * years) > member.JoinDate;
        }

        private int YearsInClan(ClanMember member)
        {
            return Convert.ToInt32(Math.Floor((DateTime.UtcNow - member.JoinDate).Days / 365.25));
        }

        private Rank CheckAdminRank(ClanMember member)
        {
            var yearsInClan = YearsInClan(member);
            if (yearsInClan == 2)
            {
                return member.Xp > 500_000_000 ? Rank.Admin : Rank.General;
            }
            else if (yearsInClan == 1)
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
                        else if (member.JoinDate != DateTime.MinValue || check.ExpectedRank != Rank.Admin)
                        {
                            output.Add(check);
                        }
                    }
                }
            }
            return output.OrderBy(m => m.CurrentRank).ThenBy(m => m.Name).ToList();
        }

        public async Task<List<GeneralRankCheck>> CheckGeneralsAsync(bool includeUnknownJoinDate)
        {
            var output = new List<GeneralRankCheck>();
            var members = (await repo.GetMembersAsync()).Where(m => m.Rank == Rank.General);
            foreach (var member in members)
            {
                var check = new GeneralRankCheck { Name = member.Name, CurrentXp = member.Xp, JoinDate = member.JoinDate };
                if (includeUnknownJoinDate || check.JoinDate != DateTime.MinValue)
                {
                    output.Add(check);
                    if (member.JoinDate != DateTime.MinValue)
                    {
                        check.YearsInClan = YearsInClan(member);
                        if (YearsInClan(member) >= 2)
                        {
                            check.RankupXp = 500_000_000 - member.Xp;
                        }
                        else
                        {
                            check.RankupXp = 1_000_000_000 - member.Xp;
                            check.RankupDate = member.JoinDate.AddYears(2);
                        }
                    }
                }
                
            }
            return output.OrderBy(m => m.JoinDate).ToList();
        }

    }
}
