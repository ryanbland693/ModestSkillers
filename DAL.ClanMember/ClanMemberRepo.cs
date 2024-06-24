using System.Net.Http.Json;

namespace DAL.ClanMember
{
    public interface IClanMemberRepo
    {
        Task<List<ClanMember>> GetMembersAsync();
    }
    public class ClanMemberRepo : IClanMemberRepo
    {
        const string url = @"https://api.runepixels.com/clans/2096/list?playertype=0&playersubtype=0";

        public async Task<List<ClanMember>> GetMembersAsync()
        {
            using HttpClient client = new HttpClient();
            var res = await client.GetFromJsonAsync<List<ClanMember>>(url);
            if (res != null)
            {
                return res;
            }
            throw new ClanMemberException("Failed to retrieve clan members");
        }

    }
}
