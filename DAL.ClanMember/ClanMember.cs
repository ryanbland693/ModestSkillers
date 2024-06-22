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

    public class Background
    {
        public Image Image { get; set; }
        public List<object> Colors { get; set; }
        public int Angle { get; set; }
        public int Padding { get; set; }
        public int Radius { get; set; }
    }

    public class Image
    {
        public bool Active { get; set; }
        public string Base64 { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class Images
    {
        public List<object> Left { get; set; }
        public List<Right> Right { get; set; }
    }

    public class Message
    {
        public string Text { get; set; }
        public string Color { get; set; }
    }

    public class Name
    {
        public int FontSize { get; set; }
        public bool Bold { get; set; }
        public List<string> Colors { get; set; }
        public int Angle { get; set; }
        public bool LetterByLetter { get; set; }
        public bool SupportAmount { get; set; }
        public Message Message { get; set; }
    }

    public class NameStyle
    {
        public Name Name { get; set; }
        public Background Background { get; set; }
        public Images Images { get; set; }
        public bool Hidden { get; set; }
    }

    public class Right
    {
        public string Base64 { get; set; }
        public int Size { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class ClanMember
    {
        public object PlayerID { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public NameStyle NameStyle { get; set; }
        public Rank Rank { get; set; }
        public long Xp { get; set; }
        public int Kills { get; set; }
        public DateTime JoinDate { get; set; }
        public DateTime LastTimeXP { get; set; }
        public DateTime LastTimeActivity { get; set; }
    }
}
