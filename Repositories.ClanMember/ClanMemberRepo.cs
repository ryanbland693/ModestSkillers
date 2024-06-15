using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ClanMember
{
    public interface IClanMemberRepo
    {
        Task<List<ClanMember>> GetMembersAsync();
    }
    public class ClanMemberRepo : IClanMemberRepo
    {
        const string url = @"https://secure.runescape.com/m=clan-hiscores/a=13/members_lite.ws?clanName=Modest Skillers";

        public async Task<List<ClanMember>> GetMembersAsync()
        {
            using HttpClient client = new HttpClient();
            var res = await client.GetStringAsync(url);
            if (!string.IsNullOrEmpty(res))
            {
                var members = new List<ClanMember>();
                var rawResponse = res.Split("\n").Skip(1);
                foreach (var line in rawResponse)
                {
                    var props = line.Split(",");
                    if (props.Length > 3)
                    {
                        members.Add(new ClanMember
                        {
                            Name = props[0],
                            Rank = (Rank)Enum.Parse(typeof(Rank), props[1].Replace(" ", "")),
                            ClanXp = long.Parse(props[2]),
                            Kills = long.Parse(props[3])
                        });
                    }
                    
                }
                return members;
            }
            else
            {
                throw new ClanMemberException("Could not retrieve clan members");
            }
        }

    }
}
