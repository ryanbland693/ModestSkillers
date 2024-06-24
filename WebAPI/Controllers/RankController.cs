using Microsoft.AspNetCore.Mvc;
using RankChecker.BusinessLogic;

namespace WebAPI.Controllers
{
    [Route("/api/rank")]
    public class RankController : ControllerBase
    {
        private readonly IRankService rankService;

        public RankController(IRankService rankService)
        {
            this.rankService = rankService;
        }
        [HttpGet("rankups")]
        public async Task<ActionResult<List<ClanMemberRankCheck>>> GetRankupsAsync([FromQuery] bool checkTooHigh, [FromQuery] bool includeUnknownJoinDates)
        {
            var rankups = await rankService.CheckRanksAsync(checkTooHigh, includeUnknownJoinDates);
            if (rankups is not null)
            {
                return Ok(rankups);
            }
            else
            {
                return BadRequest("Something went wrong");
            }
        }
        
    }
}
