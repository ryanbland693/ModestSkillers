namespace DAL.ClanMember
{
    public enum Rank
    {
        Recruit,
        Corporal,
        Sergeant,
        Lieutenant,
        Captain,
        General,
        Admin,
        Organiser,
        Coordinator,
        Overseer,
        DeputyOwner,
        Owner
    }

    public class ClanMember
    {
        public string Name { get; set; } = string.Empty;
        public Rank Rank { get; set; }
        public long ClanXp { get; set; }
        public long Kills { get; set; }
    }
}
