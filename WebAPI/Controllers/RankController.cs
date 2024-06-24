using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RankChecker.BusinessLogic;
using WebAPI.DTO;

namespace WebAPI.Controllers
{
    [Route("/api/rank")]
    public class RankController : ControllerBase
    {
        private readonly IRankService rankService;
        private readonly IMapper mapper;

        public RankController(IRankService rankService, IMapper mapper)
        {
            this.rankService = rankService;
            this.mapper = mapper;
        }
        [HttpGet("rankups")]
        public async Task<ActionResult<List<ClanMemberRankCheckDTO>>> GetRankupsAsync([FromQuery] bool checkTooHigh, [FromQuery] bool includeUnknownJoinDates)
        {
            var rankups = await rankService.CheckRanksAsync(checkTooHigh, includeUnknownJoinDates);
            if (rankups is not null)
            {
                var res = mapper.Map<List<ClanMemberRankCheckDTO>>(rankups);
                return Ok(res);
            }
            else
            {
                return BadRequest("Something went wrong");
            }
        }
        
    }
}
