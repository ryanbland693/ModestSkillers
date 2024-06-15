using DAL.ClanMember;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
