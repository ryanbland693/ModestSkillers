namespace RankChecker.BusinessLogic
{
    public class GeneralRankCheck
    {
        public string Name { get; set; }
        public long CurrentXp { get; set; }
        public DateTime JoinDate { get; set; }
        public int YearsInClan { get; set; }
        public long? RankupXp { get; set; }
        public DateTime? RankupDate { get; set; }
    }
}
