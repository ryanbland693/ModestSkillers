using RankChecker.BusinessLogic;
using DAL.ClanMember;

var repo = new ClanMemberRepo();
var rankChecker = new RankService(repo);

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

