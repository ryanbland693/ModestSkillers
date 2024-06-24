using RankChecker.BusinessLogic;
using DAL.ClanMember;

var repo = new ClanMemberRepo();
var rankChecker = new RankService(repo);

var generals = await rankChecker.CheckGeneralsAsync(true);
foreach (var gen in generals)
{
    Console.WriteLine($"Name = {string.Format("{0,-12}", gen.Name)}" +
            $"XP = {string.Format("{0,-12}", gen.CurrentXp.ToString("n0"))}" +
             $"Join Date = {string.Format("{0,-15}", gen.JoinDate.ToString("dd-MM-yyyy"))}" +
             $"Rankup XP = {string.Format("{0,-12}", gen.RankupXp)}" +
             $"Rankup Date = {string.Format("{0,-15}", gen.RankupDate)}" 
        );
}

try
{
    var IncorrectRanks = await rankChecker.CheckRanksAsync(false, false);
    if (IncorrectRanks.Count > 0)
    {
        foreach(var check in IncorrectRanks)
        {
            Console.WriteLine($"Name = {string.Format("{0,-12}", check.Name)} " +
                $"XP = {string.Format("{0,-13}", check.ClanXp.ToString("n0"))} " +
                $"Current Rank = {string.Format("{0,-12}", check.CurrentRank)}" + 
                $"Expected Rank = {string.Format("{0,-12}", check.ExpectedRank)}" + 
                (string.IsNullOrEmpty(check.Message) ? string.Empty : check.Message));
        }
    }
    else
    {
        Console.WriteLine("No incorrect ranks found");
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}

